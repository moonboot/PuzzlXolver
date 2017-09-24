using System;
using System.Diagnostics;

namespace PuzzlXolver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var saillagouse = KrydsordMix.CreatePage59();
            var solver = new DepthFirstBruteForceSolver();
            Stopwatch stopwatch = Stopwatch.StartNew();
            var solution = solver.Solve(saillagouse);
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
