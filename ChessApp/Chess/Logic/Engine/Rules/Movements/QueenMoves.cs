using Chess.Models;

namespace Chess.Logic.Engine.Rules.Movements
{
    public class QueenMoves : IRule
    {
        public bool IsMoveValid(Move move, Board board)
            => new BishopMoves().IsMoveValid(move, board)
            || new RookMoves().IsMoveValid(move, board);
    }
}
