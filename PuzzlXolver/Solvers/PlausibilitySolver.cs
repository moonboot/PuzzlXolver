using System;
using System.Linq;
using System.Collections.Generic;

namespace PuzzlXolver.Solvers
{
    public class PlausibilitySolver
    {
        public class SolverContext
        {
            public long count;
            public int depth;
            List<char?[,]> Tried = new List<char?[,]>();
            List<char?[,]>[,] TriedIndex;

            public int TriedCount => Tried.Count();

            public SolverContext(int columns, int rows)
            {
                TriedIndex = new List<char?[,]>[columns, rows];
                for (int row = 0; row < rows; row++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        TriedIndex[column, row] = new List<char?[,]>();
                    }
                }
            }

            public bool AlreadyTried(Puzzle puzzle)
            {
                var tried = FindRelevant(puzzle).FirstOrDefault(u => Matches(u, puzzle.cells));
                //if (tried != null)
                //{
                //    var lines = new List<string>();
                //    for (int row = 0; row < tried.GetLength(1); row++)
                //    {
                //        string line = "";
                //        for (int column = 0; column < tried.GetLength(0); column++)
                //        {
                //            var blankValue = ' ';
                //            line += $"{tried[column, row] ?? blankValue} ";
                //        }
                //        lines.Add(line);
                //    }
                //    //Console.WriteLine("This puzzle:");
                //    //Console.WriteLine(puzzle);
                //    //Console.WriteLine("Was eliminated by");
                //    //Console.WriteLine(string.Join(Environment.NewLine, lines));
                //    //Console.WriteLine($"{Matches(tried, puzzle.cells)}");
                //}
                return tried != null;                
            }

            public List<char?[,]> FindRelevant(Puzzle puzzle)
            {
                for (int row = 0; row < puzzle.Rows; row++)
                {
                    for (int column = 0; column < puzzle.Columns; column++)
                    {
                        if (puzzle.cells[column, row].HasValue)
                        {
                            return TriedIndex[column, row];
                        }
                    }
                }
                return new List<char?[,]>();
            }

            public void AddTried(char?[,] tried)
            {
                Tried.Add(tried);
                for (int row = 0; row < tried.GetLength(1); row++)
                {
                    for (int column = 0; column < tried.GetLength(0); column++)
                    {
                        if (tried[column, row].HasValue)
                        {
                            TriedIndex[column, row].Add(tried);
                        }
                    }
                }
            }

            // If all the tried values are also in the candidate, it will not lead to a solution
            private bool Matches(char?[,] tried, char?[,] candidate)
            {
                for (int x = 0; x < tried.GetLength(0); x++)
                {
                    for (int y = 0; y < tried.GetLength(1); y++)
                    {
                        var triedValue = tried[x, y];
                        if (!triedValue.HasValue) continue;

                        var candidateValue = candidate[x, y];
                        if (!candidateValue.HasValue || triedValue != candidateValue)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public Puzzle Solve(Puzzle puzzle)
        {
            return Solve(puzzle, new SolverContext(puzzle.Columns, puzzle.Rows));
        }

		public Puzzle Solve(Puzzle puzzle, SolverContext context)
		{
//            if (context.count % 1000 < 3) Console.Write($"\r{context.count} / {context.depth}");

			if (!puzzle.Words.Any())
			{
				return puzzle;
			}

			var part = puzzle.PartiallyFilledCellRanges.ToList();
            List<Tuple<string, List<CellRange>>> combos = new List<Tuple<string, List<CellRange>>>();

            if (part.Any(p => !puzzle.Words.Any(w => puzzle.Matches(p, w))))
            {
                return null;
            }

			foreach (var word in puzzle.Words)
            {
                combos.Add(Tuple.Create(word, part.Where(p => puzzle.Matches(p, word)).ToList()));
            }

            foreach (var combo in combos.OrderBy(c => c.Item2.Count()).ThenByDescending(c => c.Item1.Length))
            {
                var ranges = combo.Item2;

                foreach (var range in ranges)
                {
					context.count++;

                    var candidatePuzzle = puzzle.SetWord(range, combo.Item1);
                    if (candidatePuzzle == null) 
                    {
                        continue;
                    }

                    if (context.AlreadyTried(candidatePuzzle))
                    {
                        continue;
                    }

                    //var words = candidatePuzzle.Words.ToArray();
                    //var ranges = candidatePuzzle.PartiallyFilledCellRanges.Concat(candidatePuzzle.UnfilledCellRanges).ToArray();
                    //if (words.Length != ranges.Length)
                    //{
                    //    continue;
                    //}

					Console.WriteLine(candidatePuzzle);
                    Console.WriteLine($"Solving C:{context.count} D:{context.depth} T:{context.TriedCount}");
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
                        if (candidatePuzzle.IsPartialSolution())
                        {
                            Console.WriteLine("Rejected partial solution:");
                            Console.WriteLine(candidatePuzzle);
                            candidatePuzzle.IsPartialSolution();
                            Environment.Exit(-1);
                        }
						context.AddTried(candidatePuzzle.CopyOfCells());
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
    }
}
