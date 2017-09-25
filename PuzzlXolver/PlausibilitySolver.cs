using System;
using System.Linq;

namespace PuzzlXolver
{
    public class SolverContext
    {
        public long count;
        public int depth;
    }

    public class PlausibilitySolver
    {
        public Puzzle Solve(Puzzle puzzle)
        {
            return Solve(puzzle, new SolverContext());
        }

		public Puzzle Solve(Puzzle puzzle, SolverContext context)
		{
            if (context.count % 1000 < 3) Console.Write($"\r{context.count} / {context.depth}");

			if (!puzzle.Words.Any())
			{
				return puzzle;
			}

			var part = puzzle.PartiallyFilledCellRanges.ToList();
			foreach (var partiallyFilled in part)
			{
				foreach (var candidateWord in puzzle.Words.Where(w => puzzle.Matches(partiallyFilled, w)))
				{
                    context.count++;

					//Console.SetCursorPosition(0, Console.CursorTop);
					//Console.WriteLine(puzzle);
                    var candidatePuzzle = puzzle.SetWord(partiallyFilled, candidateWord);
                    if (IsPlausible(candidatePuzzle))
                    {
                        context.depth++;
						var solved = Solve(candidatePuzzle, context);
                        context.depth--;
						if (solved != null)
						{
							return solved;
						}
					}
				}
			}

			// No solution is possible
			return null;
		}

        // Rules out combinations where a particular range has no matching word
        public bool IsPlausible(Puzzle puzzle)
        {
            foreach (var partiallyFilled in puzzle.PartiallyFilledCellRanges.ToList())
            {
                if (!puzzle.Words.Any(w => puzzle.Matches(partiallyFilled, w))) return false;
            }
            return true;
        }
    }
}
