using System;
using System.Linq;
using System.Collections.Generic;

namespace PuzzlXolver
{
    public class Puzzle
    {
        public char?[,] cells;
        List<CellRange> cellRanges = new List<CellRange>();
        public List<string> Words { get; private set; }
        public char?[,] solution;

        public Puzzle(int columns, int rows, List<CellRange> cellRanges, IEnumerable<string> words)
        {
            cells = new char?[columns, rows];
            this.cellRanges = cellRanges;
            this.Words = words.ToList();
        }

        private Puzzle(char?[,] cells, List<CellRange> cellRanges, IEnumerable<string> words, char?[,] solution)
        {
            this.cells = cells;
            this.cellRanges = cellRanges;
            this.Words = words.ToList();
            this.solution = solution;
        }

        public char?[,] CopyOfCells()
        {
            var result = new char?[Columns, Rows];
            Array.Copy(cells, result, Columns * Rows);
            return result;
        }

        public int Rows => cells.GetLength(1);

        public int Columns => cells.GetLength(0);

        public Puzzle SetWord(CellRange cellRange, string word)
        {
            //if (!Matches(cellRange, word)) throw new ArgumentException("Mismatch");

            var puzzle = new Puzzle(CopyOfCells(), this.cellRanges, this.Words.Where(w => w != word), this.solution);
            puzzle.SetLetters(cellRange, word);
            return puzzle;
        }

        private void SetLetters(CellRange cellRange, string word)
        {
            if (cellRange.Direction == Direction.Horizontal)
            {
                for (int column = 0; column < cellRange.Length; column++)
                {
                    SetLetter(cellRange.OriginX + column, cellRange.OriginY, word[column]);
                }
            }
            else
            {
                for (int row = 0; row < cellRange.Length; row++)
                {
                    SetLetter(cellRange.OriginX, cellRange.OriginY + row, word[row]);
                }
            }
        }

        public string GetWord(CellRange cellRange)
        {
            return GetWord(cellRange, cells);
        }

        public static string GetWord(CellRange cellRange, char?[,] cellValues)
        {
            return new string(GetLetters(cellRange, cellValues).Select(c => c.HasValue ? c.Value : '?').ToArray());
        }

        public static IEnumerable<char?> GetLetters(CellRange cellRange, char?[,] cellValues)
        {
            if (cellRange.Direction == Direction.Horizontal)
            {
                for (int column = 0; column < cellRange.Length; column++)
                {
                    yield return cellValues[cellRange.OriginX + column, cellRange.OriginY];
                }
            }
            else
            {
                for (int row = 0; row < cellRange.Length; row++)
                {
                    yield return cellValues[cellRange.OriginX, cellRange.OriginY + row];
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
            var notFilled = cellRanges.Where(cr => GetLetters(cr, cells).Any(l => !l.HasValue)).ToList();
            
            if (Words.Count() != notFilled.Count())
            {
                throw new Exception($"Expected {cellRanges.Count()} words, but got {Words.Count()}");
            }

            var wordLengths = Words.GroupBy(w => w.Length).ToDictionary(g => g.Key, g => g.Count());
            var rangeLengths = notFilled.GroupBy(cr => cr.Length).ToDictionary(g => g.Key, g => g.Count());

            foreach (var kvp in rangeLengths)
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

            if (solution != null)
            {
                var unsedWords = Words.Concat(FilledCellRanges.Select(cr => GetWord(cr))).ToList();
                foreach (var cellRange in cellRanges)
                {
                    var word = GetWord(cellRange, solution);
                    if (!unsedWords.Remove(word))
                    {
                        throw new Exception($"Solution word '{word}' is not a valid word or it was used more than once.");
                    }
                }
            }
        }

        public CellRange FindCellRange(int column, int row, Direction direction)
        {
            return cellRanges.SingleOrDefault(cr => cr.OriginX == column && cr.OriginY == row && cr.Direction == direction);
        }

        public IEnumerable<CellRange> PartiallyFilledCellRanges => cellRanges.Where(PartiallyFilled);

        public IEnumerable<CellRange> FilledCellRanges => cellRanges.Where(c => GetLetters(c, cells).All(l => l.HasValue));

        public IEnumerable<CellRange> UnfilledCellRanges => cellRanges.Where(c => GetLetters(c, cells).All(l => !l.HasValue));

        private bool PartiallyFilled(CellRange cellRange) 
        {
            var letters = GetLetters(cellRange, cells).ToList();
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

        public void SetSolution(params string[] rows)
        {
            if (rows.Count() != this.Rows) throw new ArgumentException("Wrong number of rows", nameof(rows));
            if (rows.Any(r => r.Length != this.Columns)) throw new ArgumentException("Wrong number of columns", nameof(rows));

            this.solution = new char?[this.Columns, this.Rows];
            int rowNum = 0;
            foreach (var row in rows) {
                for (int column = 0; column < this.Columns; column++)
                {
                    char? letter = row[column];
                    if (letter == ' ') letter = null;
                    this.solution[column, rowNum] = letter;
                }
                rowNum++;
            }
        }

        public override string ToString()
        {
            var lines = new List<string>();
            for (int row = 0; row < cells.GetLength(1); row++)
            {
                string line = "";
                for (int column = 0; column < cells.GetLength(0); column++)
                {
                    var blankValue = ' '; //(cellRanges.Any(c => c.Covers(column, row))) ? '\u25A1' : ' ';
                    line += $"{cells[column, row] ?? blankValue} ";
                }
                lines.Add(line);
            }

//            lines.Add(String.Join(", ", CalculateWordLengths().OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key} letters: {kvp.Value}")));

            return string.Join(Environment.NewLine, lines);
        }
    }
}