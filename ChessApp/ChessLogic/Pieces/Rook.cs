using ChessLogic.Moves;

namespace ChessLogic.Pieces;
internal class Rook : Piece
{
    public override PieceType Type => PieceType.Rook;

    public override Player Color { get; }

    private static readonly Direction[] directions =
    [
        Direction.North,
        Direction.East,
        Direction.South,
        Direction.West,
    ];


    public Rook(Player color)
    {
        Color = color;
    }

    public override Piece Copy()
    {
        Rook copy = new(Color);
        copy.HasMoved = HasMoved;
        return copy;
    }

    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        return MovePositionsInDir(from, board, directions)
            .Select(to => new NormalMove(from, to));
    }
}
