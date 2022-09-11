namespace Chess.Models.Pieces
{
    /// <summary>
    /// Defines a pawn figure.
    /// </summary>
    public class Pawn : BasePiece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pawn"/> class.
        /// </summary>
        /// <param name="color">Color of the figure.</param>
        /// <param name="square">Square where the figure is in.</param>
        public Pawn(FigureColor color, Square? square = null)
            : base(color, square)
        {
            Figure = FigureType.Pawn;
        }

        public bool EnPassant { get; set; }

        /// <inheritdoc/>
        public override BasePiece Clone(Square square) => new Pawn(Color, square);
    }
}
