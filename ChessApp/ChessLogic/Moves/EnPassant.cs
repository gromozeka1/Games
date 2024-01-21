namespace ChessLogic.Moves;
public class EnPassant : Move
{
    public override MoveType Type => MoveType.EnPassant;

    public override Position From { get; }

    public override Position To { get; }

    private readonly Position capturePosition;

    public EnPassant(Position from, Position to)
    {
        From = from;
        To = to;
        capturePosition = new Position (from.Row, to.Column);
    }

    public override bool Execute(Board board)
    {
        new NormalMove(From, To).Execute(board);
        board[capturePosition] = null;

        return true;
    }
}
