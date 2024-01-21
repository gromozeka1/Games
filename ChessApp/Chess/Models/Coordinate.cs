using System;

namespace Chess.Models;

/// <summary>
/// Defines coordinates on the board.
/// </summary>
public class Coordinate : IEquatable<Coordinate>
{
    private int y;
    private int x;

    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    public int X
    {
        get => x;
        private set
        {
            if (value is < 0 or > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Value {value} should be from 0 to 7");
            }

            x = value;
        }
    }

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    public int Y
    {
        get => y;
        private set
        {
            if (value is < 0 or > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Value {value} should be from 0 to 7");
            }

            y = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Coordinate"/> class.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <inheritdoc/>
    public override string ToString() => (char)('A' + X) + (8 - Y).ToString();

    /// <inheritdoc/>
    public bool Equals(Coordinate? other) => other is not null
            && this.GetHashCode() == other.GetHashCode()
            && X == other.X
            && Y == other.Y;

    /// <summary>
    /// Checks for equality the two coordinates.
    /// </summary>
    /// <param name="a">First coordinate.</param>
    /// <param name="b">Second coordinate.</param>
    /// <returns>True if coordinates are equal; otherwise - false.</returns>
    public static bool operator ==(Coordinate a, Coordinate b) => a.Equals(b);

    /// <summary>
    /// Checks for unequality the two coordinates.
    /// </summary>
    /// <param name="a">First coordinate.</param>
    /// <param name="b">Second coordinate.</param>
    /// <returns>True if coordinates are not equal; otherwise - false.</returns>
    public static bool operator !=(Coordinate a, Coordinate b) => !a.Equals(b);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as Coordinate);

    /// <inheritdoc/>
    public override int GetHashCode() => X + (8 * Y);
}
