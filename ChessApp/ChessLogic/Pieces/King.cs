using ChessLogic.Moves;

namespace ChessLogic.Pieces;
public class King : Piece
{
    public override PieceType Type => PieceType.King;

    public override Player Color { get; }
    private static readonly Direction[] directions =
    [
        Direction.NorthWest,
        Direction.NorthEast,
        Direction.SouthWest,
        Direction.SouthEast,
        Direction.North,
        Direction.East,
        Direction.South,
        Direction.West,
    ];

    public King(Player color)
    {
        Color = color;
    }

    private static bool IsUnmovedRook(Position position, Board board)
    {
        if (board.IsEmpty(position))
        {
            return false;
        }

        Piece piece = board[position];
        return piece.Type == PieceType.Rook && !piece.HasMoved;
    }

    private static bool AllEmpty(IEnumerable<Position> positions, Board board)
    {
        return positions.All(board.IsEmpty);
    }

    private bool CanCastleKingSide(Position from, Board board)
    {
        if (HasMoved)
        {
            return false;
        }

        Position rookPosition = new Position(from.Row, 7);
        Position[] betweenPositions = [new(from.Row, 5), new Position(from.Row, 6)];

        return IsUnmovedRook(rookPosition, board) && AllEmpty(betweenPositions, board);
    }

    private bool CanCastleQueenSide(Position from, Board board)
    {
        if (HasMoved)
        {
            return false;
        }

        Position rookPosition = new Position(from.Row, 0);
        Position[] betweenPositions = [new(from.Row, 1), new Position(from.Row, 2), new Position(from.Row, 3)];

        return IsUnmovedRook(rookPosition, board) && AllEmpty(betweenPositions, board);
    }

    public override Piece Copy()
    {
        King copy = new(Color);
        copy.HasMoved = HasMoved;
        return copy;
    }

    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        foreach (Position to in MovePositions(from, board))
        {
            yield return new NormalMove(from, to);
        }

        if (CanCastleKingSide(from, board))
        {
            yield return new Castle(MoveType.CastleKingSide, from);
        }

        if (CanCastleQueenSide(from, board))
        {
            yield return new Castle(MoveType.CastleQueenSide, from);
        }
    }

    private IEnumerable<Position> MovePositions(Position from, Board board)
    {
        foreach (var dir in directions)
        {
            Position to = from + dir;
            if (!Board.IsInside(to))
            {
                continue;
            }

            if (board.IsEmpty(to) || board[to].Color != Color)
            {
                yield return to;
            }
        }
    }

    public override bool CanCaptureOpponentKing(Position from, Board board)
    {
        return MovePositions(from, board).Any(to =>
        {
            Piece piece = board[to];
            return piece?.Type == PieceType.King;
        });
    }
}
