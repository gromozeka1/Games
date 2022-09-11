using System;

using Chess.Models;

namespace Chess.Logic.Engine.Rules.Movements
{
    public class KnightMoves : IRule
    {
        public bool IsMoveValid(Move move, Board board) =>
            ((Math.Abs(move.To.X - move.From.X) == 2) &&
             (Math.Abs(move.To.Y - move.From.Y) == 1)) ||
            ((Math.Abs(move.To.Y - move.From.Y) == 2) &&
             (Math.Abs(move.To.X - move.From.X) == 1));
    }
}
