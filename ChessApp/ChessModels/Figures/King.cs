namespace ChessModels.Figures
{
    /// <summary>
    /// Defines a king figure.
    /// </summary>
    public class King : BaseFigure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="King"/> class.
        /// </summary>
        /// <param name="color">Color of the figure.</param>
        /// <param name="square">Square where the figure is in.</param>
        public King(Color color, Square? square = null)
            : base(color, square)
        {
            Type = Type.King;
        }

        /// <inheritdoc/>
        public override BaseFigure Clone(Square square)
            => new King(Color, square);

    }
}
