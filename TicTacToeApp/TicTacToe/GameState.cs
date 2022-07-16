using System;
using System.Linq;

namespace TicTacToe
{
    public class GameState
    {
        public Player[,] GameGrid { get; private set; } = new Player[3, 3];
        public Player CurrentPlayer { get; private set; } = Player.X;
        public int TurnsPassed { get; private set; }
        public bool GameOver { get; private set; }

        public event Action<Position>? MoveMade;
        public event Action<GameResult?>? GameEnded;
        public event Action? GameRestarted;

        public GameState()
        {
        }

        private bool CanMakeMove(Position position)
            => !GameOver && GameGrid[position.X, position.Y] == Player.None;

        private bool IsGridFull()
            => TurnsPassed == 9;

        private void SwitchPlayer()
            => CurrentPlayer = CurrentPlayer == Player.X
                ? Player.O
                : Player.X;

        private bool AreSquaresMarked((int r, int c)[] squares, Player player)
            => !squares.Where(coord => GameGrid[coord.r, coord.c] != player).Any();

        private bool IsMoveWin(Position position, out WinInfo? winInfo)
        {
            (int, int)[] row = new[] { (position.X, 0), (position.X, 1), (position.X, 2) };
            (int, int)[] column = new[] { (0, position.Y), (1, position.Y), (2, position.Y) };
            (int, int)[] mainDiagonal = new[] { (0, 0), (1, 1), (2, 2) };
            (int, int)[] antiDiagonal = new[] { (0, 2), (1, 1), (2, 0) };

            if (AreSquaresMarked(row, CurrentPlayer))
            {
                winInfo = new WinInfo { Type = WinType.Row, Number = position.X };
                return true;
            }

            if (AreSquaresMarked(column, CurrentPlayer))
            {
                winInfo = new WinInfo { Type = WinType.Column, Number = position.Y };
                return true;
            }

            if (AreSquaresMarked(mainDiagonal, CurrentPlayer))
            {
                winInfo = new WinInfo { Type = WinType.MainDiagonal };
                return true;
            }

            if (AreSquaresMarked(antiDiagonal, CurrentPlayer))
            {
                winInfo = new WinInfo { Type = WinType.AntiDiagonal };
                return true;
            }

            winInfo = null;
            return false;
        }

        private bool DidMoveEndGame(Position position, out GameResult? gameResult)
        {
            if (IsMoveWin(position, out WinInfo? winInfo))
            {
                gameResult = new GameResult { Winner = CurrentPlayer, WinInfo = winInfo };
                return true;
            }

            if (IsGridFull())
            {
                gameResult = new GameResult { Winner = Player.None };
                return true;
            }

            gameResult = null;
            return false;
        }

        public void MakeMove(Position position)
        {
            if (!CanMakeMove(position))
            {
                return;
            }

            GameGrid[position.X, position.Y] = CurrentPlayer;
            TurnsPassed++;

            if (DidMoveEndGame(position, out GameResult? gameResult))
            {
                GameOver = true;
                MoveMade?.Invoke(position);
                GameEnded?.Invoke(gameResult);
            }
            else
            {
                SwitchPlayer();
                MoveMade?.Invoke(position);
            }
        }

        public void Reset()
        {
            GameGrid = new Player[3, 3];
            CurrentPlayer = Player.X;
            TurnsPassed = 0;
            GameOver = false;
            GameRestarted?.Invoke();
        }
    }
}
