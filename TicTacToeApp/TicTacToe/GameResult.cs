namespace TicTacToe
{
    /// <summary>
    /// Provides functionality for game result.
    /// </summary>
    public class GameResult
    {
        /// <summary>
        /// Gets or sets the winner.
        /// </summary>
        public Player Winner { get; set; }
        
        /// <summary>
        /// Gets or sets info about the winner.
        /// </summary>
        public WinInfo? WinInfo { get; set; }
    }
}
