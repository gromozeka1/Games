using System;
using System.Collections.Generic;

namespace Snake
{
    public class Direction
    {
        public readonly static Direction Left = new(0, -1);
        public readonly static Direction Right = new(0, 1);
        public readonly static Direction Up = new(-1, 0);
        public readonly static Direction Down = new(1, 0);
        public int RowOffset { get; }
        public int ColumnOffset { get; }

        private Direction(int rowOffset, int columnOffset)
        {
            this.RowOffset = rowOffset;
            this.ColumnOffset = columnOffset;
        }

        public Direction Opposite()
            => new(-this.RowOffset, -ColumnOffset);

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Direction);
        }

        public bool Equals(Direction other) => other is not null
                && this.RowOffset == other.RowOffset
                && this.ColumnOffset == other.ColumnOffset;

        public static bool operator ==(Direction left, Direction right)
            => EqualityComparer<Direction>.Default.Equals(left, right);

        public static bool operator !=(Direction left, Direction right) => !(left == right);

        public override int GetHashCode() => HashCode.Combine(this.RowOffset, this.ColumnOffset);
    }
}
