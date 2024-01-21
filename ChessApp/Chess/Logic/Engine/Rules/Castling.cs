using System;
using System.Linq;

using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.Rules;

public class Castling : IRule
{
    public bool IsMoveValid(Move move, Board board)
    {
        Square targetSquare = board.SquareAt(move.To);
        BasePiece? piece = board.FigureAt(move.From);

        if ((Math.Abs(move.To.X - move.From.X) == 2) &&
            (move.To.Y == move.From.Y))
        {
            if (!NoPiecesBetween(move, board))
            {
                return false;
            }

            Coordinate coord = new Coordinate(move.To.X > move.From.X ? 7 : 0, move.From.Y);
            BasePiece? possibleRook = board.FigureAt(coord);

            return !piece.HasMoved && (!possibleRook?.HasMoved == true) && (possibleRook?.Figure == FigureType.Rook);
        }

        if ((targetSquare?.Piece?.Figure == FigureType.Rook) && (targetSquare?.Piece?.Color == piece.Color))
        {
            return NoPiecesBetween(move, board) && !piece.HasMoved && !targetSquare.Piece.HasMoved;
        }

        return true;
    }

    private static bool NoPiecesBetween(Move move, Board board) => (move.To.X > move.From.X
            ? board.Squares.OfType<Square>()
                .ToList()
                .FindAll(x => (x.Y == move.From.Y) && (x.X < 7) && (x.X > move.From.X))
            : board.Squares.OfType<Square>()
                .ToList()
                .FindAll(x => (x.Y == move.From.Y) && (x.X > 0) && (x.X < move.From.X))
    ).All(x => x.Piece == null);
}
