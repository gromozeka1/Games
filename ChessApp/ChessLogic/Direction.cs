namespace ChessLogic;

public class Direction
{
    public static readonly Direction North = new(-1, 0);
    public static readonly Direction South = new(1, 0);
    public static readonly Direction West = new(0, -1);
    public static readonly Direction East = new(0, 1);

    public static readonly Direction NorthEast = North + East;
    public static readonly Direction NorthWest = North + West;
    public static readonly Direction SouthEast = South + East;
    public static readonly Direction SouthWest = South + West;

    public int RowDelta { get; }
    public int ColumnDelta { get; }

    public Direction(int rowDelta, int columnDelta)
    {
        RowDelta = rowDelta;
        ColumnDelta = columnDelta;
    }

    public static Direction operator + (Direction left, Direction right)
    {
        return new Direction(left.RowDelta + right.RowDelta, left.ColumnDelta + right.ColumnDelta);
    }

    public static Direction operator *(int scalar, Direction direction)
    {
        return new Direction(scalar * direction.RowDelta, scalar * direction.ColumnDelta);
    }

}
