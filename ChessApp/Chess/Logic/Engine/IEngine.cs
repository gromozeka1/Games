using System.Collections.Generic;

using Chess.Logic.Engine.States;
using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine
{
    public interface IEngine
    {
        /// <summary>
        ///     Ask the engine to do a move
        /// </summary>
        /// <param name="move">The move to do</param>
        /// <returns>True if the move was valid and therefore has been done</returns>
        bool DoMove(Move move);

        List<Square> PossibleMoves(BasePiece piece);

        /// <summary>
        /// Undo the last move.
        /// </summary>
        /// <returns>True if undo was done, otherwise - false.</returns>
        Move? Undo();

        /// <summary>
        /// Redo the last move that has been redone
        /// </summary>
        /// <returns>True if redo was done, otherwise - false.</returns>
        Move? Redo();

        /// <summary>
        /// Gets current state of the game.
        /// </summary>
        /// <returns>Current state.</returns>
        public BoardState GetCurrentState();
    }
}
