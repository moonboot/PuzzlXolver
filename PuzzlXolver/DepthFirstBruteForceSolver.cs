using System;
using System.Linq;
using System.Collections.Generic;

namespace PuzzlXolver
{
    public class DepthFirstBruteForceSolver
    {
        public Puzzle Solve(Puzzle puzzle)
        {
			if (!puzzle.Words.Any())
			{
				return puzzle;
			}

            var part = puzzle.PartiallyFilledCellRanges.ToList();
			foreach (var partiallyFilled in part)
            {
                foreach (var candidateWord in puzzle.Words.Where(w => puzzle.Matches(partiallyFilled, w)))
				{
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.WriteLine(puzzle);
                    var solved = Solve(puzzle.SetWord(partiallyFilled, candidateWord));
                    if (solved != null)
                    {
                        return solved;
                    }
				}
            }

            // No solution is possible
            return null;
        }
	}
}
