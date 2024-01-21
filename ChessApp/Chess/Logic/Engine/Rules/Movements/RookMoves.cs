using System.Linq;

using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Logic.Engine.Rules.Movements;

public class RookMoves : IRule
{
    public bool IsMoveValid(Move move, Board board)
    {
        Square targetSquare = board.SquareAt(move.To);
        BasePiece? piece = board.FigureAt(move.From);
        //if the movement is not inline
        if (!((piece.Square.X == targetSquare.X) ^ (piece.Square.Y == targetSquare.Y)))
        {
            return false;
        }

        return board.Squares.OfType<Square>()
            .Where(x => piece.Square.Y == targetSquare.Y
                ? Between(piece.Square.X, targetSquare.X, x.X) && (x.Y == targetSquare.Y)
                : //Horizontal movement
                Between(piece.Square.Y, targetSquare.Y, x.Y) && (x.X == targetSquare.X)) //Vertical movement
                                                                                         //All squares in between startsquare and targetsquare are empty
            .All(betweenSquare => betweenSquare.Piece == null);
    }

    private static bool Between(int i, int j, int x)
        => i > j
        ? (i > x) && (j < x)
        : (j > x) && (i < x);
}
