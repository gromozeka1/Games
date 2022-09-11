namespace TicTacToe.EndGameLogic
{
    /// <summary>
    /// Provides functionality for draw.
    /// </summary>
    public class Draw
    {
        private readonly int turnsPassed;
        
        private bool IsGridFull() => turnsPassed == 9;
        
        public static GameResult GameResult { get; } = new() { Winner = Player.None };

        public Draw(GameState gameState) => turnsPassed = gameState.TurnsPassed;

        public bool IsDraw => IsGridFull();
    }
}
