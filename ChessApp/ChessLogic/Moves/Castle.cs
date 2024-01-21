namespace ChessLogic.Moves;
public class Castle : Move
{
    public override MoveType Type { get; }

    public override Position From { get; }

    public override Position To { get; }

    private readonly Direction kingMoveDirection;
    private readonly Position rookFromPosition;
    private readonly Position rookToPosition;

    public Castle(MoveType type, Position kingPosition)
    {
        Type = type;
        From = kingPosition;

        if (type == MoveType.CastleKingSide)
        {
            kingMoveDirection = Direction.East;
            To = new Position(kingPosition.Row, 6);
            rookFromPosition = new Position(kingPosition.Row, 7);
            rookToPosition = new Position(kingPosition.Row, 5);
        }
        else if (type == MoveType.CastleQueenSide)
        {
            kingMoveDirection = Direction.West;
            To = new Position(kingPosition.Row, 2);
            rookFromPosition = new Position(kingPosition.Row, 0);
            rookToPosition = new Position(kingPosition.Row, 3);
        }
    }

    public override bool Execute(Board board)
    {
        new NormalMove(From, To).Execute(board);
        new NormalMove(rookFromPosition, rookToPosition).Execute(board);

        return false;
    }

    public override bool IsLegal(Board board)
    {
        Player player = board[From].Color;

        if (board.IsInCheck(player))
        {
            return false;
        }

        Board copy = board.Copy();
        Position kingPositionInCopy = From;

        for (int i = 0; i < 2; i++)
        {
            new NormalMove(kingPositionInCopy, kingPositionInCopy + kingMoveDirection).Execute(copy);
            kingPositionInCopy += kingMoveDirection;

            if (copy.IsInCheck(player))
            {
                return false;
            }
        }

        return true;
    }
}
