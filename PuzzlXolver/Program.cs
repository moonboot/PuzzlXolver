using System;
using System.Diagnostics;
using PuzzlXolver.Solvers;

namespace PuzzlXolver
{
    public class Program
    {
        public static void Main()
        {
            var puzzle = KrydsordMix.CreatePage59();
			Console.WriteLine(puzzle);

            var solver = new PlausibilitySolverWithPreelimination();// new BreadthFirstPlausibilitySolver();
            Stopwatch stopwatch = Stopwatch.StartNew();
            var solution = solver.Solve(puzzle);
            if (solution != null)
            {
				Console.WriteLine(solution);
			}
            else
            {
                Console.WriteLine("Not solved :-(");
			}
            Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");
        }
    }
}
