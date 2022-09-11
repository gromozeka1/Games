namespace TicTacToe
{
    /// <summary>
    /// Provides functionality for WinInfo.
    /// </summary>
    public class WinInfo
    {
        /// <summary>
        /// Gets or sets information about WinType.
        /// </summary>
        public WinType Type { get; set; }

        /// <summary>
        /// Gets or sets number of winning row or column.
        /// </summary>
        public int Number { get; set; }
    }
}
