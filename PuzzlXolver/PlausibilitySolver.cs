using System;
using System.Linq;
using System.Collections.Generic;

namespace PuzzlXolver
{
    public class SolverContext
    {
        public long count;
        public int depth;
        public List<char?[,]> Tried = new List<char?[,]>();
    }

    public class PlausibilitySolver
    {
        public Puzzle Solve(Puzzle puzzle)
        {
            return Solve(puzzle, new SolverContext());
        }

		public Puzzle Solve(Puzzle puzzle, SolverContext context)
		{
//            if (context.count % 1000 < 3) Console.Write($"\r{context.count} / {context.depth}");

			if (!puzzle.Words.Any())
			{
				return puzzle;
			}

			var part = puzzle.PartiallyFilledCellRanges.ToList();
			foreach (var partiallyFilled in part)
			{
                var candidates = puzzle.Words.Where(w => puzzle.Matches(partiallyFilled, w)).ToList();
				foreach (var candidateWord in candidates)
				{
                    context.count++;

                    var candidatePuzzle = puzzle.SetWord(partiallyFilled, candidateWord);
                    if (context.Tried.Any(u => Matches(u, candidatePuzzle.cells))) continue;
					context.Tried.Add(candidatePuzzle.CopyOfCells());

					Console.WriteLine(candidatePuzzle);
					var isPlausible = IsPlausible(candidatePuzzle);
					if (isPlausible)
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

			if (!puzzle.Words.Any())
			{
				return puzzle;
			}

			// No solution is possible
			return null;
		}

        // Rules out combinations where a particular range has no matching word
        public bool IsPlausible(Puzzle puzzle)
        {
            var part = puzzle.PartiallyFilledCellRanges.ToList();
            var words = puzzle.Words.ToList();
            if (part.Count() > words.Count()) {
                throw new ArgumentException($"Word[{words.Count()}]/cellrange[{part.Count()}] mismatch");
            }

            foreach (var partiallyFilled in part)
            {
                bool foundMatch = false;
                foreach (var word in words)
                {
                    if (puzzle.Matches(partiallyFilled, word))
                    {
                        foundMatch = true;
                        break;
                    }
                }
                if (!foundMatch) return false;
            }
            return true;
        }

        private bool Matches(char?[,] cells1, char?[,] cells2)
        {
            for (int x = 0; x < cells1.GetLength(0); x++)
            {
                for (int y = 0; y < cells1.GetLength(1); y++)
                {
                    if (cells1[x, y] != cells2[x, y]) return false;
                }
            }
            return true;
        }
    }
}
