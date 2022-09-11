using ChessModels.Figures;

namespace ChessModels
{
    public class Square
    {
        /// <summary>
        /// Gets or sets the figure on the square.
        /// </summary>
        public BaseFigure? Figure { get; set; }

        /// <summary>
        /// Gets a coordinate.
        /// </summary>
        public Coordinate Coordinate { get; private set; }

        /// <summary>
        /// Gets the board where the square is in.
        /// </summary>
        public Board Board { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Square"/> class.
        /// </summary>
        /// <param name="parent">The parent board.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public Square(Board parent, Coordinate coordinate)
        {
            Board = parent;
            Coordinate = coordinate;
        }
    }
}
