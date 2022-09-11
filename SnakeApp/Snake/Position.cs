using System;
using System.Collections.Generic;

namespace Snake
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public Position Translate(Direction direction)
        {
            return new Position(Row + direction.RowOffset, Column + direction.ColumnOffset);
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   this.Row == position.Row &&
                   this.Column == position.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Row, this.Column);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
    }
}
