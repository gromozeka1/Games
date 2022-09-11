using Chess.Models.Pieces;

namespace Chess.Models
{
    /// <summary>
    /// Defines the chess board.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class.
        /// </summary>
        /// <param name="size"></param>
        public Board(int size = 8)
        {
            Size = size;
            Squares = new Square[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Squares[i, j] = new Square(this, i, j);
                }
            }

            EightByEightInit();
            // PatTestInit();
        }

        public Board(Board board)
        {
            Size = board.Size;
            Squares = new Square[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Square square = new Square(this, i, j);
                    square.Piece = board.Squares[i, j]?.Piece?.Clone(square);
                    Squares[i, j] = square;
                }
            }
        }

        public int Size { get; set; }

        public Square[,] Squares { get; set; }

        /// <summary>
        /// Gets a square with the coordinate.
        /// </summary>
        /// <param name="coordinate">Coordinate of the square</param>
        /// <returns>The square at the coordinate</returns>
        public Square SquareAt(Coordinate coordinate) => Squares[coordinate.X, coordinate.Y];

        /// <summary>
        /// </summary>
        /// <param name="coordinate">Coordinate of the square</param>
        /// <returns>The piece at the coordinate</returns>
        public BasePiece? FigureAt(Coordinate coordinate) => SquareAt(coordinate).Piece;

        private void EightByEightInit()
        {
            for (int i = 0; i < Size; i++)
            {
                Squares[i, 1].Piece = new Pawn(FigureColor.Black);
                Squares[i, 6].Piece = new Pawn(FigureColor.White);
            }

            Squares[0, 0].Piece = new Rook(FigureColor.Black);
            Squares[1, 0].Piece = new Knight(FigureColor.Black);
            Squares[2, 0].Piece = new Bishop(FigureColor.Black);
            Squares[3, 0].Piece = new Queen(FigureColor.Black);
            Squares[4, 0].Piece = new King(FigureColor.Black);
            Squares[5, 0].Piece = new Bishop(FigureColor.Black);
            Squares[6, 0].Piece = new Knight(FigureColor.Black);
            Squares[7, 0].Piece = new Rook(FigureColor.Black);

            Squares[0, 7].Piece = new Rook(FigureColor.White);
            Squares[1, 7].Piece = new Knight(FigureColor.White);
            Squares[2, 7].Piece = new Bishop(FigureColor.White);
            Squares[3, 7].Piece = new Queen(FigureColor.White);
            Squares[4, 7].Piece = new King(FigureColor.White);
            Squares[5, 7].Piece = new Bishop(FigureColor.White);
            Squares[6, 7].Piece = new Knight(FigureColor.White);
            Squares[7, 7].Piece = new Rook(FigureColor.White);
        }

        private void PatTestInit()
        {
            Squares[7, 0].Piece = new King(FigureColor.Black, Squares[7, 0]);
            Squares[0, 0].Piece = new King(FigureColor.White, Squares[0, 0]);
            Squares[2, 5].Piece = new Rook(FigureColor.White, Squares[2, 5]);
            Squares[3, 5].Piece = new Rook(FigureColor.White, Squares[3, 5]);
        }
    }
}
