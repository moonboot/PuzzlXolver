using System;

namespace PuzzlXolver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var saillagouse = KrydsordMix.CreatePage59();
            var solver = new Solver();
            if (solver.Solve(saillagouse))
            {
				Console.WriteLine(saillagouse);
			}
            else
            {
                Console.WriteLine("Not solved :-(");
			}
        }
    }
}
