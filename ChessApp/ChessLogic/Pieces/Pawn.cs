using ChessLogic.Moves;

namespace ChessLogic.Pieces;
public class Pawn : Piece
{
    public override PieceType Type => PieceType.Pawn;

    public override Player Color { get; }

    private readonly Direction forward;

    public Pawn(Player color)
    {
        Color = color;
        if (Color == Player.White)
        {
            forward = Direction.North;
        }
        else if(Color == Player.Black)
        {
            forward = Direction.South;
        }
    }

    public override Piece Copy()
    {
        Pawn copy = new(Color);
        copy.HasMoved = HasMoved;
        return copy;
    }

    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        return ForwardMoves(from, board)
            .Concat(DiagonalMoves(from, board));
    }

    private static bool CanMoveTo(Position to, Board board)
    {
        return Board.IsInside(to) && board.IsEmpty(to);
    }

    private bool CanCaptureAt(Position to, Board board)
    {
        if (!Board.IsInside(to) || board.IsEmpty(to))
        {
            return false;
        }

        return board[to].Color != Color;
    }

    private static IEnumerable<Move> PromotionMoves(Position from, Position to)
    {
        yield return new PawnPromotion(from, to, PieceType.Knight);
        yield return new PawnPromotion(from, to, PieceType.Bishop);
        yield return new PawnPromotion(from, to, PieceType.Rook);
        yield return new PawnPromotion(from, to, PieceType.Queen);
    }

    private IEnumerable<Move> ForwardMoves(Position from, Board board)
    {
        Position oneMovePosition = from + forward;
        if (CanMoveTo(oneMovePosition, board))
        {
            if (oneMovePosition.Row == 0 || oneMovePosition.Row == 7)
            {
                foreach (Move promMove in PromotionMoves(from, oneMovePosition))
                {
                    yield return promMove;
                }
            }
            else
            {
                yield return new NormalMove(from, oneMovePosition);

                Position twoMovesPosition = oneMovePosition + forward;
                if (!HasMoved && CanMoveTo(twoMovesPosition, board))
                {
                    yield return new DoublePawn(from, twoMovesPosition);
                }
            }
        }
    }

    private IEnumerable<Move> DiagonalMoves(Position from, Board board)
    {
        foreach (var direction in new Direction[] { Direction.West, Direction.East })
        {
            Position to = from + forward + direction;

            if (to == board.GetPawnSkipPosition(Color.Opponent()))
            {
                yield return new EnPassant(from, to);
            }
            else if (CanCaptureAt(to, board))
            {
                if (to.Row == 0 || to.Row == 7)
                {
                    foreach (Move promMove in PromotionMoves(from, to))
                    {
                        yield return promMove;
                    }
                }
                else
                {
                    yield return new NormalMove(from, to);
                }
            }
        }
    }

    public override bool CanCaptureOpponentKing(Position from, Board board)
    {
        return DiagonalMoves(from, board).Any(move =>
        {
            Piece piece = board[move.To];
            return piece?.Type == PieceType.King;
        });
    }
}
