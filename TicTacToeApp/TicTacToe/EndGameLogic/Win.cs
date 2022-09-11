using System.Linq;

namespace TicTacToe.EndGameLogic
{
    public class Win
    {
        private readonly Player[,] gameGrid;

        private readonly Player currentPlayer;
        
        public WinInfo? WinInfo { get; private set; }

        private static readonly (int, int)[] MainDiagonalWin = new[] { (0, 0), (1, 1), (2, 2) };
        
        private static readonly (int, int)[] AntiDiagonalWin = new[] { (0, 2), (1, 1), (2, 0) };

        public GameResult? GameResult => new()
        {
            Winner = currentPlayer,
            WinInfo = WinInfo
        };

        public Win(GameState gameState)
        {
            gameGrid = gameState.GameGrid;
            currentPlayer = gameState.CurrentPlayer;
        }

        public bool IsMoveWin(Location position)
            => IsRowWin(position)
            || IsColumnWin(position)
            || IsMainDiagonalWin()
            || IsAntiDiagonalWin();

        private bool AreSquaresMarked((int r, int c)[] squares, Player player)
            => !squares.Where(coord => gameGrid[coord.r, coord.c] != player).Any();

        private bool IsRowWin(Location position)
        {
            var rowWin = new[] { (position.X, 0), (position.X, 1), (position.X, 2) };
            if (AreSquaresMarked(rowWin, currentPlayer))
            {
                WinInfo = new WinInfo { Type = WinType.Row, Number = position.X };
                return true;
            }

            return false;
        }

        private bool IsColumnWin(Location position)
        {
            var columnWin = new[] { (0, position.Y), (1, position.Y), (2, position.Y) };
            bool condition = AreSquaresMarked(columnWin, currentPlayer);
            if (condition)
            {
                WinInfo = new WinInfo { Type = WinType.Column, Number = position.Y };
            }

            return condition;
        }

        private bool IsMainDiagonalWin()
        {
            bool condition = AreSquaresMarked(MainDiagonalWin, currentPlayer);
            if (condition)
            {
                WinInfo = new WinInfo { Type = WinType.MainDiagonal };
            }

            return condition;
        }

        private bool IsAntiDiagonalWin()
        {
            bool condition = AreSquaresMarked(AntiDiagonalWin, currentPlayer);
            if (condition)
            {
                WinInfo = new WinInfo { Type = WinType.AntiDiagonal };
            }

            return condition;
        }
    }
}
