using Chess.Models.Pieces;
using Chess.Models;

namespace Chess.Logic.Engine.Rules.Movements;

public class PawnMoves : IRule
{
    public bool IsMoveValid(Move move, Board board)
    {
        Square targetSquare = board.SquareAt(move.To);
        BasePiece? piece = board.FigureAt(move.From);
        Square square = board.SquareAt(move.From);
        bool isWhite = piece.Color == FigureColor.White;
        bool isStartPosition = ((piece.Square.Y == 1) && !isWhite) || ((piece.Square.Y == 6) && isWhite);

        if (targetSquare.Piece is null)
        {
            bool normalMove = ((piece.Square.Y - targetSquare.Y == (isWhite ? 1 : -1))
                    || (isStartPosition && (piece.Square.Y - targetSquare.Y == (isWhite ? 2 : -2))))
                    && (piece.Square.X == targetSquare.X)
                    && (board.Squares[square.X, isWhite ? square.Y - 1 : square.Y + 1].Piece == null)
                ;

            Pawn? leftPiece =
                square.X > 0
                    ? board.Squares[square.X - 1, square.Y]?.Piece as Pawn
                    : null;
            
            Pawn? rightPiece =
                square.X < 7
                    ? board.Squares[square.X + 1, square.Y]?.Piece as Pawn
                    : null;

            if (leftPiece?.EnPassant == true && leftPiece.Color != piece.Color)
            {
                if ((targetSquare.X == square.X - 1) && (piece.Square.Y - targetSquare.Y == (isWhite ? 1 : -1)))
                {
                    return true;
                }
            }

            if (rightPiece?.EnPassant == true && rightPiece.Color != piece.Color)
            {
                if ((targetSquare.X == square.X + 1) && (piece.Square.Y - targetSquare.Y == (isWhite ? 1 : -1)))
                {
                    return true;
                }
            }

            return normalMove;
        }

        return ((piece.Square.X == targetSquare.X - 1) || (piece.Square.X == targetSquare.X + 1))
            && (piece.Square.Y - targetSquare.Y == (isWhite ? 1 : -1));
    }
}
