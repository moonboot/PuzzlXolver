using System;
using System.Linq;
using System.Collections.Generic;

namespace PuzzlXolver
{
	public class Puzzle
	{
		Cell[,] cells;
		List<CellRange> cellRanges = new List<CellRange>();
        public PuzzleState State { get; set; }

		public IReadOnlyList<CellRange> CellRanges { get; private set; }

		public Puzzle(int columns, int rows, IEnumerable<string> words)
		{
			cells = new Cell[columns, rows];
			State = new PuzzleState(columns, rows, words);
			for (int column = 0; column < columns; column++)
			{
				for (int row = 0; row < rows; row++)
				{
					cells[column, row] = new Cell(this, column, row);
				}
			}
			CellRanges = cellRanges.AsReadOnly();
		}

		public void AddCellRange(int originX, int originY, int length, Direction direction)
		{
			var rangeCells = new List<Cell>();
			if (direction == Direction.Horizontal)
			{
				for (int column = 0; column < length; column++)
				{
					rangeCells.Add(cells[originX + column, originY]);
				}
			}
			else
			{
				for (int row = 0; row < length; row++)
				{
					rangeCells.Add(cells[originX, originY + row]);
				}
			}
			foreach (var cell in rangeCells)
			{
				cell.SetUsed();
			}
			cellRanges.Add(new CellRange(originX, originY, length, direction, rangeCells));
		}

		private Dictionary<int, int> CalculateWordLengths()
		{
			var result = new Dictionary<int, int>();
			foreach (var cellRange in CellRanges)
			{
				int length = cellRange.Cells.Count;
				var count = result.GetOrAdd(length, () => 0);
				result[length] = count + 1;
			}
			return result;
		}

		internal void Verify()
		{
			var wordLengths = CalculateWordLengths();
			var rangeLength = CellRanges.GroupBy(cr => cr.Cells.Count).ToDictionary(g => g.Key, g => g.Count());

			foreach (var kvp in rangeLength)
			{
				if (wordLengths[kvp.Key] != kvp.Value)
				{
					throw new Exception($"Expected {kvp.Value} words of length {kvp.Key}, but got {wordLengths[kvp.Key]}");
				}
			}
		}

		public void SetValue(int column, int row, char value)
		{
			State.SetValue(column, row, value);
		}

		public char GetValue(int column, int row)
		{
			return State.GetValue(column, row);
		}

		public override string ToString()
		{
			var lines = new List<string>();
			for (int row = 0; row < cells.GetLength(1); row++)
			{
				string line = "";
				for (int column = 0; column < cells.GetLength(0); column++)
				{
					line += $"{cells[column, row]} ";
				}
				lines.Add(line);
			}

			lines.Add(String.Join(", ", CalculateWordLengths().OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key} letters: {kvp.Value}")));

			return string.Join(Environment.NewLine, lines);
		}
	}
}