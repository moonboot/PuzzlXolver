using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzlXolver
{
    public class CellRange
    {
        private int originX, originY;
        private int length;
        private Direction direction;

        public List<Cell> Cells { get; private set; }

        internal CellRange(int originX, int originY, int length, Direction direction, List<Cell> cells)
        {
			this.originX = originX;
			this.originY = originY;
            this.length = length;
            this.direction = direction;
            Cells = cells;
        }

        public override string ToString()
        {
            return new string(Cells.Select(c => c.Value).ToArray());
        }

        public void SetWord(string word)
        {
            if (Cells.Count != word.Length) throw new ArgumentException(nameof(word), $"Word should have length {Cells.Count}, was {word}");

            for (int pos = 0; pos < Cells.Count; pos++)
            {
                if (Cells[pos].State != CellState.Filled)
                {
                    Cells[pos].Value = word[pos];
                }

                if (Cells[pos].State == CellState.Filled && Cells[pos].Value != word[pos])
                {
                    var cellString = new string(Cells.Select(c => c.Value).ToArray());
                    throw new ArgumentException(nameof(word), $"Word can not be used. Mismatch at position {pos} between {cellString} and {word}");
                }
            }
        }
    }
}
