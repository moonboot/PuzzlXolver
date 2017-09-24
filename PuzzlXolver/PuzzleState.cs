using System;
using System.Collections.Generic;

namespace PuzzlXolver
{
    public class PuzzleState
    {
        char[,] cellValues;
        public List<string> UnusedWords { get; private set; }

        public PuzzleState(int columns, int rows, IEnumerable<string> unusedWords) {
			cellValues = new char[columns, rows];
			this.UnusedWords = new List<string>(unusedWords);
		}

        public PuzzleState(PuzzleState copyFrom)
        {
            cellValues = new char[copyFrom.cellValues.GetLength(0), copyFrom.cellValues.GetLength(1)];
			for (int column = 0; column < cellValues.GetLength(0); column++)
			{
				for (int row = 0; row < cellValues.GetLength(1); row++)
				{
                    cellValues[column, row] = copyFrom.cellValues[column, row];
				}
			}

			this.UnusedWords = new List<string>(copyFrom.UnusedWords);
		}

        public void SetValue(int column, int row, char value) {
            cellValues[column, row] = value;
        }

        public char GetValue(int column, int row) {
            return cellValues[column, row];
        }

        public void MarkWordAsUsed(string word)
        {
            if (!UnusedWords.Contains(word)) throw new ArgumentOutOfRangeException(nameof(word), word, "Not an unused word");

            UnusedWords.Remove(word);
        }
    }
}
