namespace Chess.GameLogic;

public class Square : IEquatable<Square>
{
    public static Square None => new(-1, -1);

    private const char FirstLetter = 'a';
    private const char LastLetter = 'h';
    private const char FirstNumber = '1';
    private const char LastNumber = '8';
    public int X { get; private set; }
    public int Y { get; private set; }

    public string Name => ((char)('a' + X)).ToString() + (1 + Y).ToString();
    public Square(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Square(string position) // e2
    {
        if (!IsValidSquare(position))
        {
            throw new ArgumentException("Incorrect position", nameof(position));
        }

        X = position[0] - FirstLetter;
        Y = position[1] - FirstNumber;
    }

    private static bool IsValidSquare(string position) => position.Length == 2
            && position[0] >= FirstLetter
            && position[0] <= LastLetter
            && position[1] >= FirstNumber
            && position[1] <= LastNumber;

    public static bool operator ==(Square a, Square b)
        => a.Equals(b);

    public static bool operator !=(Square a, Square b)
        => !a.Equals(b);

    public bool OnBoard()
        => X >= 0 && X < 8 
        && Y >= 0 && Y < 8;

    public bool Equals(Square? other)
        => other is not null && GetHashCode() == other.GetHashCode()
        && X == other.X && Y == other.Y;

    public override bool Equals(object? obj) => Equals(obj as Square);

    public override int GetHashCode() => HashCode.Combine(X, Y);
}
