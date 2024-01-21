using ChessLogic.Pieces;

namespace ChessLogic.Moves;
public class PawnPromotion : Move
{
    public override MoveType Type => MoveType.PawnPromotion;

    public override Position From { get; }

    public override Position To { get; }

    private readonly PieceType newType;

    public PawnPromotion(Position from, Position to, PieceType newType)
    {
        From = from;
        To = to;
        this.newType = newType;
    }

    private Piece CreatePromotionPiece(Player color)
    {
        return newType switch
        {
            PieceType.Knight => new Knight(color),
            PieceType.Bishop => new Bishop(color),
            PieceType.Rook => new Rook(color),
            _ => new Queen(color),
        };
    }

    public override bool Execute(Board board)
    {
        Piece pawn = board[From];
        board[From] = null;

        Piece promotionPiece = CreatePromotionPiece(pawn.Color);
        promotionPiece.HasMoved = true;
        board[To] = promotionPiece;

        return true;
    }
}
