using ChessLogic.Moves;

namespace ChessLogic.Pieces;
public class Queen : Piece
{
    public override PieceType Type => PieceType.Queen;

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

    public Queen(Player color)
    {
        Color = color;
    }

    public override Piece Copy()
    {
        Queen copy = new(Color);
        copy.HasMoved = HasMoved;
        return copy;
    }

    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        return MovePositionsInDir(from, board, directions)
            .Select(to => new NormalMove(from, to));
    }
}
