using System.ComponentModel;
using System.Runtime.CompilerServices;

using Chess.Models.Pieces;

namespace Chess.Models
{
    /// <summary>
    /// Defines a square on the board.
    /// </summary>
    public class Square
    {
        /// <summary>
        /// A figure on the square.
        /// </summary>
        private BasePiece? piece;

        /// <summary>
        /// Gets or sets the figure on the square.
        /// </summary>
        public BasePiece? Piece
        {
            get => piece;
            set
            {
                piece = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets X position.
        /// </summary>
        public int X { get; set; }
        
        /// <summary>
        /// Gets or sets Y position.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets a coordinate.
        /// </summary>
        public Coordinate Coordinate => new(X, Y);

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
        public Square(Board parent, int x, int y)
        {
            Board = parent;
            X = x;
            Y = y;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc/>
        public override string ToString() => (char)('A' + X) + (8 - Y).ToString();

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
