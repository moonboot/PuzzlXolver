using System;
using System.Linq;
using System.Collections.Generic;

namespace PuzzlXolver
{
    public class Solver
    {
        public Solver()
        {
        }

        public bool Solve(Puzzle puzzle)
        {
            foreach (var partiallyFilled in puzzle.CellRanges.Where(cr => cr.PartiallyFilled()))
            {
                foreach (var candidateWord in puzzle.State.UnusedWords.Where(w => partiallyFilled.Matches(w)))
				{
                    var currentState = puzzle.State;
                    var newState = new PuzzleState(currentState);
                    puzzle.State = newState;
                    partiallyFilled.SetWord(candidateWord);
                    newState.MarkWordAsUsed(candidateWord);
					Console.WriteLine(puzzle);
					if (!newState.UnusedWords.Any())
                    {
						Console.WriteLine(puzzle);
                        return true;
					}
                    if (Solve(puzzle))
                    {
                        // We found it!
                        return true;
                    }
                    // Not a good solution - try again
                    puzzle.State = currentState;
				}
            }
            // No solution is possible
            return false;
        }
	}
}
