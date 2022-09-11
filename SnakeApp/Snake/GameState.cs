using System;
using System.Collections.Generic;

namespace Snake
{
    public class GameState
    {
        public int Rows { get; }
        public int Columns { get; }

        public GridValue[,] Grid { get; }

        public Direction Direction { get; private set; }

        public int Score { get; private set; }

        public bool GameOver { get; private set; }

        private readonly LinkedList<Direction> dirChanges = new LinkedList<Direction>();
        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();
        private readonly Random random = new Random();

        public GameState(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.Grid = new GridValue[rows, columns];
            this.Direction = Direction.Right;

            AddSnake();
            AddFood();
        }

        private void AddSnake()
        {
            int r = Rows / 2;

            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake;
                snakePositions.AddFirst(new Position(r, c));

            }
        }

        private IEnumerable<Position> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Grid[r,c] == GridValue.Empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPositions());
            if (empty.Count == 0)
            {
                return;
            }

            Position position = empty[random.Next(empty.Count)];
            Grid[position.Row, position.Column] = GridValue.Food;
        }

        public Position HeadPosition() => snakePositions.First.Value;

        public Position TailPosition() => snakePositions.Last.Value;

        public IEnumerable<Position> SnakePositions() => snakePositions;

        public void AddHead(Position head)
        {
            snakePositions.AddFirst(head);
            Grid[head.Row, head.Column] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = snakePositions.Last.Value;
            Grid[tail.Row, tail.Column] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        private Direction GetLastDirection()
        {
            return dirChanges.Count == 0 ? Direction : dirChanges.Last.Value;
        }

        private bool CanChangeDirection(Direction newDirection)
        {
            Direction lastDirection = GetLastDirection();

            return dirChanges.Count != 2
                && newDirection != lastDirection
                && newDirection != lastDirection.Opposite();
        }

        public void ChangeDirection(Direction dir)
        {
            if (CanChangeDirection(dir))
            {
                dirChanges.AddLast(dir);
            }
        }

        private bool OutsideGrid(Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows ||
                pos.Column < 0 || pos.Column >= Columns;
        }

        private GridValue WillHit(Position newHeadPosition)
        {
            if (OutsideGrid(newHeadPosition))
            {
                return GridValue.Outside;
            }

            if (newHeadPosition == TailPosition())
            {
                return GridValue.Empty;
            }

            return Grid[newHeadPosition.Row, newHeadPosition.Column];
        }

        public void Move()
        {
            if (dirChanges.Count > 0)
            {
                Direction = dirChanges.First.Value;
                dirChanges.RemoveFirst();
            }

            Position newHeadPosition = HeadPosition().Translate(Direction);
            GridValue hit = WillHit(newHeadPosition);

            if (hit is GridValue.Outside or GridValue.Snake)
            {
                GameOver = true;
            }
            else if(hit is GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPosition);
            }
            else if (hit is GridValue.Food)
            {
                AddHead(newHeadPosition);
                Score++;
                AddFood();
            }
        }
    }
}
