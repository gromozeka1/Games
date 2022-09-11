namespace ChessModels
{
    public class Coordinate : IEquatable<Coordinate>
    {
        private readonly int y;
        private readonly int x;

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public int X
        {
            get => x;
            init
            {
                if (value is < 0 or > 7)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Value {value} should be from 0 to 7");
                }

                x = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public int Y
        {
            get => y;
            init
            {
                if (value is < 0 or > 7)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Value {value} should be from 0 to 7");
                }

                y = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <inheritdoc/>
        public bool Equals(Coordinate? other) => other is not null
                && this.GetHashCode() == other.GetHashCode()
                && this.X == other.X && this.Y == other.Y;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => Equals(obj as Coordinate);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}
