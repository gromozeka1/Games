using ChessModels.Figures;

namespace ChessModels
{
    public class Board
    {
        public static int Size => 8;

        public Square[,] Squares { get; private set; } = new Square[Size, Size];

        public Color MoveColor { get; private set; } = Color.White;

        public int FullMoveNumber => HalfMoveNumber % 2;

        public int HalfMoveNumber { get; private set; }

        public string? Fen { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class.
        /// </summary>
        /// <param name="size"></param>
        public Board()
        {
            InitSquares();
            EightByEightInit();
            // PatTestInit();
        }

        public Board(Board board)
        {
            InitSquaresWithFigures(board);
        }

        public Board(string fen)
        {
            Fen = fen;
            InitByFen();
        }

        private void InitByFen()
        {
            // "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
            //  0                                           1 2    3 4 5
            if (!IsFenValid(out string[] parts))
            {
                throw new ArgumentException("Invalid format of fen.");
            }

            InitFigures(parts[0]);
            InitColor(parts[1]);
            InitNumber(parts[4], parts[5]);
        }

        private void InitNumber(string halfMove, string fullMove)
        {
            HalfMoveNumber = int.Parse(halfMove);
            if (int.Parse(fullMove) != HalfMoveNumber % 2)
            {
                throw new ArgumentException("Invalid format of fen.");
            }
        }

        private void InitColor(string color)
            => MoveColor = color == "b" ? Color.Black : Color.White;

        private void InitFigures(string figures)
        {
            for (int j = 8; j >= 2; j--)
            {
                figures = figures.Replace(j.ToString(), (j - 1).ToString() + "1");
            }

            string[] lines = figures.Split('/');
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Squares[i, j] = new(this, new Coordinate(i, j))
                    {
                        Figure = lines[i][j] switch
                        {
                            'k' => new King(Color.Black),
                            'q' => new Queen(Color.Black),
                            'r' => new Rook(Color.Black),
                            'b' => new Bishop(Color.Black),
                            'n' => new Knight(Color.Black),
                            'p' => new Pawn(Color.Black),

                            'K' => new King(Color.White),
                            'Q' => new Queen(Color.White),
                            'R' => new Rook(Color.White),
                            'B' => new Bishop(Color.White),
                            'N' => new Knight(Color.White),
                            'P' => new Pawn(Color.White),

                            _ => null,
                        }
                    };
                }
            }
        }

        private bool IsFenValid(out string[] parts)
        {
            parts = Fen!.Split();
            return parts.Length == 6;
        }

        private void InitSquares()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Coordinate coordinate = new Coordinate(i, j);
                    Squares[i, j] = new Square(this, coordinate);
                }
            }
        }

        private void InitSquaresWithFigures(Board board)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Coordinate coordinate = new Coordinate(i, j);
                    Square square = new Square(this, coordinate);
                    square.Figure = board.Squares[i, j].Figure?.Clone(square);
                    Squares[i, j] = square;
                }
            }
        }

        /// <summary>
        /// Gets a square with the coordinate.
        /// </summary>
        /// <param name="coordinate">Coordinate of the square</param>
        /// <returns>The square at the coordinate</returns>
        public Square SquareAt(Coordinate coordinate) => Squares[coordinate.X, coordinate.Y];

        /// <summary>
        /// </summary>
        /// <param name="coordinate">Coordinate of the square</param>
        /// <returns>The figure at the coordinate, if no figure - null.</returns>
        public BaseFigure? GetFigureAt(Coordinate coordinate) => SquareAt(coordinate).Figure;

        public void SetFigureAt(Coordinate coordinate, BaseFigure figure)
            => Squares[coordinate.X, coordinate.Y].Figure = figure;

        private void EightByEightInit()
        {
            for (int i = 0; i < Size; i++)
            {
                Squares[i, 1].Figure = new Pawn(Color.Black);
                Squares[i, 6].Figure = new Pawn(Color.White);
            }

            Squares[0, 0].Figure = new Rook(Color.Black);
            Squares[1, 0].Figure = new Knight(Color.Black);
            Squares[2, 0].Figure = new Bishop(Color.Black);
            Squares[3, 0].Figure = new Queen(Color.Black);
            Squares[4, 0].Figure = new King(Color.Black);
            Squares[5, 0].Figure = new Bishop(Color.Black);
            Squares[6, 0].Figure = new Knight(Color.Black);
            Squares[7, 0].Figure = new Rook(Color.Black);

            Squares[0, 7].Figure = new Rook(Color.White);
            Squares[1, 7].Figure = new Knight(Color.White);
            Squares[2, 7].Figure = new Bishop(Color.White);
            Squares[3, 7].Figure = new Queen(Color.White);
            Squares[4, 7].Figure = new King(Color.White);
            Squares[5, 7].Figure = new Bishop(Color.White);
            Squares[6, 7].Figure = new Knight(Color.White);
            Squares[7, 7].Figure = new Rook(Color.White);
        }

        private void PatTestInit()
        {
            Squares[7, 0].Figure = new King(Color.Black);
            Squares[0, 0].Figure = new King(Color.White);
            Squares[2, 5].Figure = new Rook(Color.White);
            Squares[3, 5].Figure = new Rook(Color.White);
        }
    }
}
