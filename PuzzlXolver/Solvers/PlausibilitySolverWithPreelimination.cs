using System;
using System.Linq;

namespace PuzzlXolver.Solvers
{
    public class PlausibilitySolverWithPreelimination
    {
        public Puzzle Solve(Puzzle puzzle)
        {
            var context = new PlausibilitySolver.SolverContext(puzzle.Columns, puzzle.Rows);
            Preeliminate(puzzle, context);
            return new PlausibilitySolver().Solve(puzzle, context, puzzle.FilledCellRanges.ToList());
        }

        public void Preeliminate(Puzzle puzzle, PlausibilitySolver.SolverContext context)
        {
            foreach (var cellRange in puzzle.NotFullyFilledCellRanges)
            {
                foreach (var word in puzzle.Words.Where(w => puzzle.Matches(cellRange, w)))
                {
                    // Run elimination descent for each possible start combination
                    var candidate = puzzle.SetWord(cellRange, word);
                    Preeliminate(candidate, context, cellRange);
                }
            }
        }

        public void Preeliminate(Puzzle puzzle, PlausibilitySolver.SolverContext context, CellRange previousRange)
        {
            //Console.WriteLine(puzzle);
            if (context.TriedCount % 500 == 0) Console.Write($"\rEliminating C:{context.count} D:{context.depth} T:{context.TriedCount}");

            var overlappingRanges = puzzle.NotFullyFilledCellRanges.Where(r => !r.Equals(previousRange) && r.Overlaps(previousRange)).ToList();
            foreach (var cellRange in overlappingRanges)
            {
                var words = puzzle.Words.Where(w => puzzle.Matches(cellRange, w)).ToList();
                if (words.Count() == 0)
                {
                    if (puzzle.IsPartialSolution())
                    {
                        Console.WriteLine("Rejected partial solution:");
                        Console.WriteLine(puzzle);
                        puzzle.IsPartialSolution();
                        Environment.Exit(-1);
                    }
                    context.AddTried(puzzle.cells);
                    return;
                }

                if (context.depth >= 1) return;

                foreach (var word in words)
                {
                    var candidate = puzzle.SetWord(cellRange, word);
                    if (!context.AlreadyTried(candidate))
                    {
                        context.depth++;
                        Preeliminate(candidate, context, cellRange);
                        context.depth--;
                    }
                }
            }
        }
    }
}
