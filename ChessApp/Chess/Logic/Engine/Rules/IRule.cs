using System.Collections.Generic;
using System.Linq;

using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.Rules
{
    /// <summary>
    /// Defines the chess rules.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Checks if the move is correct against the rule.
        /// </summary>
        /// <param name="move">Move to check.</param>
        /// <param name="board">Board to apply the move on.</param>
        /// <returns>True if move is valid, otherwise - false.</returns>
        public bool IsMoveValid(Move move, Board board);

        /// <summary>
        /// Gets a list of possible moves for the figure.
        /// </summary>
        /// <param name="piece">Figure.</param>
        /// <returns>A list of possible moves.</returns>
        public IEnumerable<Square> PossibleMoves(BasePiece piece)
        {
            IEnumerable<Square> squares = piece.Square!.Board.Squares.OfType<Square>();
            Board board = piece.Square.Board;
            foreach (Square square in squares)
            {
                Move move = new Move(piece, square);
                if (IsMoveValid(move, board))
                {
                    yield return square;
                }
            }
        }
    }
}
