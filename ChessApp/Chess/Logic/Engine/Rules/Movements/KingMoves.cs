using System;

using Chess.Models;

namespace Chess.Logic.Engine.Rules.Movements;

public class KingMoves : IRule
{
    public bool IsMoveValid(Move move, Board board)
    {
        int deltaX = Math.Abs(move.From.X - move.To.X);
        int deltaY = Math.Abs(move.From.Y - move.To.Y);

        return deltaX <= 1
            && deltaY <= 1;
    }

    private static bool NoFriendlyPiecesInEnd(Move move, Board board)
        => board.FigureAt(move.To).Color != move.Color;
}
