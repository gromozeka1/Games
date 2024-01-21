using ChessLogic.Moves;

namespace ChessLogic.Pieces;
public abstract class Piece
{
    public abstract PieceType Type { get; }
    public abstract Player Color { get; }
    public bool HasMoved {  get; set; } = false;
    public abstract Piece Copy();
    public abstract IEnumerable<Move> GetMoves(Position from, Board board);

    protected  IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction direction)
    {
        for (Position position = from + direction; Board.IsInside(position); position += direction)
        {
            if (board.IsEmpty(position))
            {
                yield return position;
                continue;
            }

            Piece piece = board[position];
            if (piece.Color != Color)
            {
                yield return position;
            }

            yield break;
        }
    }

    protected IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction[] directions)
    {
        return directions.SelectMany(dir => MovePositionsInDir(from, board, dir));
    }

    public virtual bool CanCaptureOpponentKing(Position from, Board board)
    {
        return GetMoves(from, board).Any(move =>
        {
            Piece piece = board[move.To];
            return piece?.Type == PieceType.King;
        });
    }
}
