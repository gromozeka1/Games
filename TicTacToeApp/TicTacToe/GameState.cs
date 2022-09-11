using System;

using TicTacToe.EndGameLogic;

namespace TicTacToe
{
    /// <summary>
    /// Provides functionality for game state.
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Gets or sets the game grid.
        /// </summary>
        public Player[,] GameGrid { get; private set; } = new Player[3, 3];
        
        /// <summary>
        /// Gets or sets the current player.
        /// </summary>
        public Player CurrentPlayer { get; private set; } = Player.X;
        
        /// <summary>
        /// Gets or sets the number of passed turns.
        /// </summary>
        public int TurnsPassed { get; private set; }
        
        /// <summary>
        /// Gets or sets the game over status.
        /// </summary>
        public bool GameOver { get; private set; }

        public event Action<Location>? MoveMade;
        public event Action<GameResult?>? GameEnded;
        public event Action? GameRestarted;

        private bool CanMakeMove(Location position)
            => !GameOver && GameGrid[position.X, position.Y] == Player.None;

        private void SwitchPlayer()
            => CurrentPlayer = CurrentPlayer == Player.X
                ? Player.O
                : Player.X;

        private bool IsMoveEndGame(Location position, out GameResult? gameResult)
        {
            var endGameLogic = new EndGame(this);
            bool condition = endGameLogic.IsGameEnd(position);
            gameResult = condition ? endGameLogic.GameResult : null;
            return condition;
        }

        /// <summary>
        /// Makes the move.
        /// </summary>
        /// <param name="position">Position.</param>
        public void MakeMove(Location position)
        {
            if (!CanMakeMove(position))
            {
                return;
            }

            GameGrid[position.X, position.Y] = CurrentPlayer;
            TurnsPassed++;

            if (IsMoveEndGame(position, out GameResult? gameResult))
            {
                GameOver = true;
                MoveMade?.Invoke(position);
                GameEnded?.Invoke(gameResult);
            }
            else
            {
                SwitchPlayer();
                MoveMade?.Invoke(position);
            }
        }

        /// <summary>
        /// Resets the game state to initial state.
        /// </summary>
        public void Reset()
        {
            GameGrid = new Player[3, 3];
            CurrentPlayer = Player.X;
            TurnsPassed = 0;
            GameOver = false;
            GameRestarted?.Invoke();
        }
    }
}
