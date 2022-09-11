namespace TicTacToe
{
    public class Location
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="x">X location (row).</param>
        /// <param name="y">Y location (column).</param>
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets or sets X location (row).
        /// </summary>
        public int X { get; set; }
        
        /// <summary>
        /// Gets or sets Y location (column).
        /// </summary>
        public int Y { get; set; }
    }
}
