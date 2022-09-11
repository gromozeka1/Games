using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.States
{
    public interface IState
    {
        /// <summary>
        /// Verifies if the state is true.
        /// </summary>
        /// <param name="board">Board.</param>
        /// <param name="color">Color.</param>
        /// <returns>Returns true if the state is true, otherwis - false.</returns>
        bool IsInState(Board board, FigureColor color);

        /// <summary>
        /// Explanation text.
        /// </summary>
        /// <returns>Explanation text.</returns>
        string Explain();
    }
}
