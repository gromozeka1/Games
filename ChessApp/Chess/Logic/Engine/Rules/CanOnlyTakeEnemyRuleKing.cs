using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.Rules
{
    public class CanOnlyTakeEnemyRuleKing : IRule
    {
        public bool IsMoveValid(Move move, Board board)
        {
            BasePiece? piece = board.FigureAt(move.To);
            return piece is null
                || NoFriendlyPiecesInEnd(piece, move)
                || IsCastlingPossible(piece, move);
        }

        private static bool NoFriendlyPiecesInEnd(BasePiece piece, Move move)
            => piece.Color != move.Color;

        private bool IsCastlingPossible(BasePiece piece, Move move)
            => piece.Color == move.Color
            && piece.Figure == FigureType.Rook;
    }
}