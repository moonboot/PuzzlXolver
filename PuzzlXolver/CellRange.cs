using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzlXolver
{
    public class CellRange
    {
        public int OriginX, OriginY;
        public int Length;
        public Direction Direction { get; private set; }

        public CellRange(int originX, int originY, int length, Direction direction)
        {
			this.OriginX = originX;
			this.OriginY = originY;
            this.Length = length;
            this.Direction = direction;
        }

        public string ToString(Puzzle puzzle)
        {
            return puzzle.GetWord(this);
        }

        public bool Covers(int x, int y)
        {
            if (x == OriginX && Direction == Direction.Vertical) {
                return y >= OriginY && y < OriginY + Length;
            }
            if (y == OriginY && Direction == Direction.Horizontal)
			{
				return x >= OriginX && x < OriginX + Length;
			}
            return false;
		}

        public bool Overlaps(CellRange other)
        {
            // TODO: Optimize - it should not be necessary to check every cell
            if (Direction == Direction.Horizontal)
            {
                for (int column = 0; column < Length; column++)
                {
                    if (other.Covers(OriginX + column, OriginY)) return true;
                }
            }
            else
            {
                for (int row = 0; row < Length; row++)
                {
                    if (other.Covers(OriginX, OriginY + row)) return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return string.Format($"{OriginX}, {OriginY}, {Length}, {Direction}", Direction);
        }

        public override bool Equals(object obj)
        {
            var other = (CellRange)obj;
            return this.OriginX == other.OriginX 
                       && this.OriginY == other.OriginY 
                       && this.Length == other.Length 
                       && this.Direction == other.Direction;
        }

        public override int GetHashCode()
        {
            return OriginX + OriginY + Length + (int)Direction;
        }
	}
}
