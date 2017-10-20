using System;
using System.Linq;
using System.Collections.Generic;

namespace PuzzlXolver.Solvers
{
    public class BreadthFirstPlausibilitySolver
    {
        public class SolverContext
        {
            public Queue<Puzzle> queue = new Queue<Puzzle>();
            public long count;
        }

        public Puzzle Solve(Puzzle puzzle)
        {
            var context = new SolverContext();
            context.queue.Enqueue(puzzle);
            while (context.queue.Any())
            {
                var candidate = context.queue.Dequeue();
                if (!candidate.Words.Any())
                {
                    return candidate;
                }
                else 
                {
                    GenerateLeads(candidate, context);
                }
            }
            return null;
        }

		public void GenerateLeads(Puzzle puzzle, SolverContext context)
		{
//            if (context.count % 1000 < 3) Console.Write($"\r{context.count} / {context.depth}");
			var part = puzzle.PartiallyFilledCellRanges.ToList();
            List<Tuple<string, List<CellRange>>> combos = new List<Tuple<string, List<CellRange>>>();

			foreach (var word in puzzle.Words)
            {
                combos.Add(Tuple.Create(word, part.Where(p => puzzle.Matches(p, word)).ToList()));
            }

            if (!combos.Any())
            {
                if (puzzle.IsPartialSolution())
                {
                    Console.WriteLine("Rejected partial solution:");
                    Console.WriteLine(puzzle);
                    Environment.Exit(-1);
                }
            }

            foreach (var combo in combos.OrderBy(c => c.Item2.Count()).ThenByDescending(c => c.Item1.Length))
            {
                foreach (var range in combo.Item2)
                {
					context.count++;

                    var candidatePuzzle = puzzle.SetWord(range, combo.Item1);
                    if (candidatePuzzle == null) 
                    {
                        continue;
                    }

					var isPlausible = IsPlausible(candidatePuzzle);
                    Console.WriteLine(candidatePuzzle);
                    Console.WriteLine($"Queue length: {context.queue.Count}");
					if (isPlausible)
					{
                        context.queue.Enqueue(candidatePuzzle);
					}
				}
			}
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

        // If all the tried values are also in the candidate, it will not lead to a solution
        private bool Matches(char?[,] tried, char?[,] candidate)
        {
            for (int x = 0; x < tried.GetLength(0); x++)
            {
                for (int y = 0; y < tried.GetLength(1); y++)
                {
                    var triedValue = tried[x, y];
                    var candidateValue = candidate[x, y];
                    if (triedValue.HasValue && candidateValue.HasValue && triedValue != candidateValue) return false;
                }
            }
            return true;
        }
    }
}
