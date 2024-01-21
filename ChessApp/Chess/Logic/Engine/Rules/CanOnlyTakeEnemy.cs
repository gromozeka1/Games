using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.Rules;

public class CanOnlyTakeEnemy : IRule
{
    public bool IsMoveValid(Move move, Board board)
    {
        BasePiece? piece = board.FigureAt(move.To);
        return piece is null
            || NoFriendlyPiecesInEnd(piece, move);
    }

    private static bool NoFriendlyPiecesInEnd(BasePiece piece, Move move)
        => piece.Color != move.Color;
}
