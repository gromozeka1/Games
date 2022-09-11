using System.Collections.Generic;
using System.Linq;

namespace Tetris.Blocks
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }

        protected abstract Position StartOffset { get; }

        public abstract int Id { get; }
        private int rotationState;
        private readonly Position offset;

        public Block() => offset = new Position(StartOffset.Row, StartOffset.Column);

        public IEnumerable<Position> TilePositions()
            => Tiles[rotationState].Select(p => new Position(p.Row + offset.Row, p.Column + offset.Column));

        public void RotateClockWise()
            => rotationState = (rotationState + 1) % Tiles.Length;

        public void RotateCounterClockWise()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}
