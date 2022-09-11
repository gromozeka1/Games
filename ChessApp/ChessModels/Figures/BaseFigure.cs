namespace ChessModels.Figures
{
    /// <summary>
    /// Defines a base figure.
    /// </summary>
    public abstract class BaseFigure
    {
        /// <summary>
        /// Initilizes a new instance of the <see cref="BaseFigure"/> class.
        /// </summary>
        /// <param name="color">Color of the figure.</param>
        /// <param name="square">Square where figure is in.</param>
        protected BaseFigure(Color color, Square? square = null)
        {
            Color = color;
            Square = square;
        }

        /// <summary>
        /// Gets or sets the type of the figure.
        /// </summary>
        public Type Type { get; protected set; }

        /// <summary>
        /// Gets or sets the color of the figure.
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Gets or sets the square where the figure is in.
        /// </summary>
        public Square? Square { get; set; }

        /// <summary>
        /// Makes a clone of the figure on the square (to copy from another board).
        /// </summary>
        /// <param name="square">Square.</param>
        /// <returns>The figure.</returns>
        public abstract BaseFigure Clone(Square square);
    }
}