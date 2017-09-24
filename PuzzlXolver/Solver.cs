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
            foreach (var anchorRange in puzzle.CellRanges.Where(PartiallyFilled))
            {
                foreach (var intersectRange in puzzle.CellRanges.Where(r => Intersects(r, anchorRange)))
                {
                    var sharedCell = SharedCell(anchorRange, intersectRange);
                    var anchorPos = anchorRange.Cells.IndexOf(sharedCell);
					var intersectPos = intersectRange.Cells.IndexOf(sharedCell);

					foreach (var candidateWord in puzzle.State.UnusedWords.Where(
                           w => w.Length == intersectRange.Cells.Count
                        && w[intersectPos] == anchorRange.Cells[anchorPos].Value))
					{
                        var currentState = puzzle.State;
                        var newState = new PuzzleState(currentState);
                        puzzle.State = newState;
                        intersectRange.SetWord(candidateWord);
                        newState.MarkWordAsUsed(candidateWord);
                        var success = Solve(puzzle);
                        if (success)
                        {
                            // We found it!
                            return true;
                        }
                        else
                        {
                            // Not a good solution - try again
                            puzzle.State = currentState;
                        }
					}
				}
            }
            // No solution is possible
            return false;
        }

        public bool PartiallyFilled(CellRange cellRange) 
        {
            return cellRange.Cells.Any(c => c.State == CellState.Filled) && cellRange.Cells.Any(c => c.State != CellState.Filled);
        }

        public bool Intersects(CellRange candidate, CellRange anchorRange)
        {
            return SharedCell(candidate, anchorRange) != null;
        }

		public Cell SharedCell(CellRange candidate, CellRange anchorRange)
		{
			return candidate.Cells.Where(c => c.State == CellState.Filled).SingleOrDefault(c => anchorRange.Cells.Contains(c));
		}
	}
}
