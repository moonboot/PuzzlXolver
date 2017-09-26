﻿using System;
using System.Diagnostics;

namespace PuzzlXolver
{
    public class Program
    {
        public static void Main(string[] args)
        {
			//            var puzzle = KrydsordMix.CreatePage9();
            var puzzle = KrydsordMix.CreatePage59();
			Console.WriteLine(puzzle);

            var solver = new PlausibilitySolver();
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
