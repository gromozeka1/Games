namespace ChessModels.Figures
{
    /// <summary>
    /// Defines a bishop figure.
    /// </summary>
    public class Bishop : BaseFigure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bishop"/> class.
        /// </summary>
        /// <param name="color">Color of the figure.</param>
        /// <param name="square">Square where the figure is in.</param>
        public Bishop(Color color, Square? square = null)
            : base(color, square)
        {
            Type = Type.Bishop;
        }

        /// <inheritdoc/>
        public override BaseFigure Clone(Square square)
            => new Bishop(Color, square);
    }
}
