using NUnit.Framework;
using System.Linq;
using System;
using System.Collections.Generic;

namespace PuzzlXolver.Test
{
    [TestFixture]
    public class UnitTests
    {
        //  + - 0 - +
        //  |       |
        //  1       2
        //  |       |
        //  + - 3 - +
        [Test]
        public void SharedCells()
        {
            var cellRanges = new List<CellRange>
            {
                new CellRange(0, 0, 3, Direction.Horizontal),
                new CellRange(0, 0, 3, Direction.Vertical),
                new CellRange(2, 0, 3, Direction.Vertical),
                new CellRange(0, 2, 3, Direction.Horizontal),
            };

            var puzzle = new Puzzle(3, 3, cellRanges, new List<string>());

            Assert.That(puzzle.ToString(), Is.EqualTo("      \n      \n      "));
        }

        [Test]
        public void PrintPage59()
        {
            var saillagouse = KrydsordMix.CreatePage59();
            Console.WriteLine(saillagouse);
        }

        [Test]
        public void PrintRoyce()
        {
            var rolls = KrydsordMix.CreateRoyce();
            Console.WriteLine(rolls);
        }

        [Test]
        public void PrintPage9()
        {
            var xxx = KrydsordMix.CreatePage9();
            Console.WriteLine(xxx);
        }

        [Test]
        public void NoAnchorWordIsNotSolvable()
        {
            var cellRanges = new List<CellRange>
            {
                new CellRange(0, 0, 3, Direction.Horizontal),
                new CellRange(0, 0, 3, Direction.Vertical),
                new CellRange(2, 0, 3, Direction.Vertical),
                new CellRange(0, 2, 3, Direction.Horizontal)
            };

            var words = new List<string> { "ABC", "ABE", "CDE", "EGE" };
            var puzzle = new Puzzle(3, 3, cellRanges, words);

            var solver = new DepthFirstBruteForceSolver();
            Assert.That(solver.Solve(puzzle), Is.Null, "With no anchor words, puzzle is not solvable");
        }

        [Test]
        public void SolvePuzzle()
        {
            var cellRanges = new List<CellRange>
            {
                new CellRange(0, 0, 3, Direction.Horizontal),
                new CellRange(0, 0, 3, Direction.Vertical),
                new CellRange(2, 0, 3, Direction.Vertical),
                new CellRange(0, 2, 3, Direction.Horizontal)
            };

            var words = new List<string> { "ABC", "ABE", "CDE", "EGE" };
            var puzzle = new Puzzle(3, 3, cellRanges, words);

            puzzle = puzzle.SetWord(cellRanges[0], "ABC");

            puzzle.Verify();

            var solver = new DepthFirstBruteForceSolver();
            var solution = solver.Solve(puzzle);
            Assert.That(solution, Is.Not.Null);

            Assert.That(solution.GetWord(cellRanges[0]), Is.EqualTo(words[0]));
            Assert.That(solution.GetWord(cellRanges[1]), Is.EqualTo(words[1]));
            Assert.That(solution.GetWord(cellRanges[2]), Is.EqualTo(words[2]));
            Assert.That(solution.GetWord(cellRanges[3]), Is.EqualTo(words[3]));
        }

        [Test]
        public void Covers()
        {
            var cr = new CellRange(2, 4, 6, Direction.Horizontal);
            Assert.That(cr.Covers(1, 4), Is.False);
            Assert.That(cr.Covers(2, 4), Is.True);
            Assert.That(cr.Covers(7, 4), Is.True);
            Assert.That(cr.Covers(8, 4), Is.False);
            Assert.That(cr.Covers(5, 5), Is.False);
        }

        [Test]
        public void VerifySolution()
        {
            KrydsordMix.CreatePage9().Verify();
            KrydsordMix.CreatePage59().Verify();
        }
    }
}