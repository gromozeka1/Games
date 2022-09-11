namespace TicTacToe.EndGameLogic
{
    /// <summary>
    /// Provides functionality for EndGame.
    /// </summary>
    public class EndGame
    {
        private readonly GameState GameState;
        
        public GameResult? GameResult { get; private set; }
        
        public EndGame(GameState gameState)
        {
            GameState = gameState;
        }
        
        /// <summary>
        /// Checks if game ends.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <returns>True if game ends, otherwise - false.</returns>
        public bool IsGameEnd(Location position)
        {
            Win winLogic = new Win(GameState);
            if (winLogic.IsMoveWin(position))
            {
                GameResult = winLogic.GameResult;
                return true;
            }

            Draw drawLogic = new Draw(GameState);
            if (drawLogic.IsDraw)
            {
                GameResult = Draw.GameResult;
                return true;
            }

            return false;
        }
    }
}
