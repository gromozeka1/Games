namespace Chess.Models.Pieces
{
    /// <summary>
    /// Defines a base figure.
    /// </summary>
    public abstract class BasePiece
    {
        /// <summary>
        /// Initilizes a new instance of the <see cref="BasePiece"/> class.
        /// </summary>
        /// <param name="color">Color of the figure.</param>
        /// <param name="square">Square where figure is in.</param>
        protected BasePiece(FigureColor color, Square? square = null)
        {
            Color = color;
            Square = square;
        }

        /// <summary>
        /// Gets or sets the color of the figure.
        /// </summary>
        public FigureColor Color { get; private set; }

        /// <summary>
        /// Gets or sets the square where the figure is in.
        /// </summary>
        public Square? Square { get; set; }

        /// <summary>
        /// Gets or sets if the piece has moved.
        /// </summary>
        public bool HasMoved { get; set; } = false;

        /// <summary>
        /// Gets or sets the type of the figure.
        /// </summary>
        public FigureType? Figure { get; set; }

        /// <summary>
        /// Makes a clone of the figure on the square.
        /// </summary>
        /// <param name="square">Square.</param>
        /// <returns>The figure.</returns>
        public abstract BasePiece Clone(Square square);

        /// <inheritdoc/>
        public override string ToString() => $"{GetType().Name}";
    }
}
