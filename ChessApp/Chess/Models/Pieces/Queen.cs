namespace Chess.Models.Pieces
{
    /// <summary>
    /// Defines a queen figure.
    /// </summary>
    public class Queen : BasePiece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Queen"/> class.
        /// </summary>
        /// <param name="color">Color of the figure.</param>
        /// <param name="square">Square where the figure is in.</param>
        public Queen(FigureColor color, Square? square = null)
            : base(color, square)
        {
            Figure = FigureType.Queen;
        }

        /// <inheritdoc/>
        public override BasePiece Clone(Square square) => new Queen(Color, square);
    }
}
