using System;
using System.Linq;
using System.Collections.Generic;

namespace PuzzlXolver
{
	public class Puzzle
	{
		char?[,] cells;
		List<CellRange> cellRanges = new List<CellRange>();
        public List<string> Words { get; private set; }

		public Puzzle(int columns, int rows, List<CellRange> cellRanges, IEnumerable<string> words)
		{
			cells = new char?[columns, rows];
			this.cellRanges = cellRanges;
            this.Words = words.ToList();
		}

		private Puzzle(char?[,] cells, List<CellRange> cellRanges, IEnumerable<string> words)
		{
            this.cells = new char?[cells.GetLength(0), cells.GetLength(1)];
            Array.Copy(cells, this.cells, cells.GetLength(0) * cells.GetLength(1));
			this.cellRanges = cellRanges;
			this.Words = words.ToList();
		}

		public Puzzle SetWord(CellRange cellRange, string word)
		{
            //if (!Matches(cellRange, word)) throw new ArgumentException("Mismatch");

            var puzzle = new Puzzle(this.cells, this.cellRanges, this.Words.Where(w => w != word));
			puzzle.SetWord(cellRange.OriginX, cellRange.OriginY, cellRange.Length, cellRange.Direction, word);
            return puzzle;
		}

		private void SetWord(int originX, int originY, int length, Direction direction, string word)
		{
			if (direction == Direction.Horizontal)
			{
				for (int column = 0; column < length; column++)
				{
                    SetLetter(originX + column, originY, word[column]);
				}
			}
			else
			{
				for (int row = 0; row < length; row++)
				{
                    SetLetter(originX, originY + row, word[row]);
				}
			}
		}

        public string GetWord(CellRange cellRange)
        {
            return new string(GetLetters(cellRange).Select(c => c.HasValue ? c.Value : '?').ToArray());
        }

		public IEnumerable<char?> GetLetters(CellRange cellRange)
        {
			if (cellRange.Direction == Direction.Horizontal)
			{
				for (int column = 0; column < cellRange.Length; column++)
				{
                    yield return cells[cellRange.OriginX + column, cellRange.OriginY];
				}
			}
			else
			{
				for (int row = 0; row < cellRange.Length; row++)
				{
					yield return cells[cellRange.OriginX, cellRange.OriginY + row];
				}
			}
		}

        private void SetLetter(int x, int y, char letter)
        {
            //if (cells[x, y].HasValue)
            //{
            //    if (cells[x, y] != letter) throw new ArgumentException(nameof(letter), $"Can not set letter '{letter}' at {x}, {y} where there is already '{cells[x, y]}'");
            //}
            //else
            {
                cells[x, y] = letter;
            }
        }

		private Dictionary<int, int> CalculateWordLengths()
		{
			var result = new Dictionary<int, int>();
			foreach (var cellRange in cellRanges)
			{
				int length = cellRange.Length;
				var count = result.GetOrAdd(length, () => 0);
				result[length] = count + 1;
			}
			return result;
		}

		public void Verify()
		{
			var wordLengths = CalculateWordLengths();
			var rangeLength = cellRanges.GroupBy(cr => cr.Length).ToDictionary(g => g.Key, g => g.Count());

			foreach (var kvp in rangeLength)
			{
				if (wordLengths[kvp.Key] != kvp.Value)
				{
					throw new Exception($"Expected {kvp.Value} words of length {kvp.Key}, but got {wordLengths[kvp.Key]}");
				}
			}

            if (!cellRanges.Any(cr => PartiallyFilled(cr)))
            {
				throw new Exception($"Expected at least one initial anchor word");
			}
		}

        public CellRange FindCellRange(int column, int row, Direction direction)
        {
            return cellRanges.SingleOrDefault(cr => cr.OriginX == column && cr.OriginY == row && cr.Direction == direction);
        }

        public IEnumerable<CellRange> PartiallyFilledCellRanges => cellRanges.Where(PartiallyFilled);

        private bool PartiallyFilled(CellRange cellRange) 
        {
            var letters = this.GetLetters(cellRange).ToList();
            return letters.Any(c => c.HasValue) && letters.Any(c => !c.HasValue);
		}

        public bool Matches(CellRange cellRange, string word)
        {
            if (cellRange.Length != word.Length) return false;

			if (cellRange.Direction == Direction.Horizontal)
			{
				for (int column = 0; column < cellRange.Length; column++)
				{
                    if (!CheckLetter(cellRange.OriginX + column, cellRange.OriginY, word[column])) return false;
				}
			}
			else
			{
				for (int row = 0; row < cellRange.Length; row++)
				{
                    if (!CheckLetter(cellRange.OriginX, cellRange.OriginY + row, word[row])) return false;
				}
			}

            return true;
		}

        private bool CheckLetter(int x, int y, char letter)
        {
            return !cells[x, y].HasValue || cells[x, y] == letter;
        }

        public override string ToString()
		{
			var lines = new List<string>();
			for (int row = 0; row < cells.GetLength(1); row++)
			{
				string line = "";
				for (int column = 0; column < cells.GetLength(0); column++)
				{
					line += $"{cells[column, row] ?? ' '} ";
				}
				lines.Add(line);
			}

//			lines.Add(String.Join(", ", CalculateWordLengths().OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key} letters: {kvp.Value}")));

			return string.Join(Environment.NewLine, lines);
		}
	}
}