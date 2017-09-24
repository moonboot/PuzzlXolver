using NUnit.Framework;
using System.Linq;
using System;
using System.Collections.Generic;

namespace PuzzlXolver.Test
{
    [TestFixture]
    public class UnitTests
    {
        //  + - 0 - A
        //  |       |
        //  1       2
        //  |       |
        //  + - 3 - +
        [Test]
        public void SharedCells()
        {
            var puzzle = new Puzzle(3, 3, new List<string>());
            puzzle.AddCellRange(0, 0, 3, Direction.Horizontal);
            puzzle.AddCellRange(0, 0, 3, Direction.Vertical);
            puzzle.AddCellRange(2, 0, 3, Direction.Vertical);
            puzzle.AddCellRange(0, 2, 3, Direction.Horizontal);

            Assert.That(puzzle.CellRanges, Has.Count.EqualTo(4));

            puzzle.CellRanges[0].Cells.Last().Value = 'A';
            Assert.That(puzzle.CellRanges[2].Cells.First().Value, Is.EqualTo('A'));

            Console.WriteLine("Puzzle:");
            Console.WriteLine(puzzle);
        }

        // G A L L A
        // N       G
        // A       I
        // G       L
        // S K Ø R E
        [Test]
        public void SimplePuzzle()
        {
            var words = new[] { "GALLA", "AGILE", "GNAGS", "SKØRE" };
            var puzzle = new Puzzle(5, 5, words);
        }

        [Test]
        public void PrintPage59()
        {
            var saillagouse = KrydsordMix.CreatePage59();
            Console.WriteLine(saillagouse);
        }

        [Test]
        public void NoAnchorWordIsNotSolvable()
        {
            var words = new List<string> { "ABC", "ABE", "CDE", "EGE" };
            var puzzle = new Puzzle(3, 3, words);
            puzzle.AddCellRange(0, 0, 3, Direction.Horizontal);
            puzzle.AddCellRange(0, 0, 3, Direction.Vertical);
            puzzle.AddCellRange(2, 0, 3, Direction.Vertical);
            puzzle.AddCellRange(0, 2, 3, Direction.Horizontal);

            var solver = new Solver();
            Assert.That(solver.Solve(puzzle), Is.False, "With no anchor words, puzzle is not solvable");
        }

        [Test]
        public void SolvePuzzle()
        {
            var words = new List<string> { "ABC", "ABE", "CDE", "EGE" };
            var puzzle = new Puzzle(3, 3, words);
            puzzle.AddCellRange(0, 0, 3, Direction.Horizontal);
            puzzle.AddCellRange(0, 0, 3, Direction.Vertical);
            puzzle.AddCellRange(2, 0, 3, Direction.Vertical);
            puzzle.AddCellRange(0, 2, 3, Direction.Horizontal);

            puzzle.CellRanges[0].SetWord(words[0]);
            puzzle.State.MarkWordAsUsed(words[0]);

            var solver = new Solver();
            Assert.That(solver.Solve(puzzle), Is.True);

            Assert.That(puzzle.CellRanges[0].ToString(), Is.EqualTo(words[0]));
            Assert.That(puzzle.CellRanges[1].ToString(), Is.EqualTo(words[1]));
            Assert.That(puzzle.CellRanges[2].ToString(), Is.EqualTo(words[2]));
            Assert.That(puzzle.CellRanges[3].ToString(), Is.EqualTo(words[3]));
        }
    }
}