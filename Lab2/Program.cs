using Common.Models;
using System;
using System.Collections.Generic;

namespace Lab2
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Point> points;
            List<int> tList;
            Console.WriteLine("Variant 1");

            points = new List<Point>
            {
                new Point(7.1, 7.03), new Point(2.79, 7.54), new Point(3.34, 5.17), new Point(7.2, 3.06),
                new Point(5.92, 7.28), new Point(4.44, 3.73), new Point(-4.47, 6.41), new Point(-6.42, 7.56),
                new Point(-7.25, 6.16), new Point(-8.45, 4.28), new Point(-2.89, 2.69), new Point(-5.8, 5.06),
                new Point(-5.92, 2.69), new Point(0.74, -2.66), new Point(-1.29, -2.94), new Point(-2.89, -5.71),
                new Point(2.37, -7.23), new Point(-2.02, -8.42), new Point(-1.62, -5.12), new Point(2.29, -5.12),
                new Point(0.54, -6.4)
            };
            tList = new List<int> { 2, 5, 7, 9, 12 };

            UseHeuristicAlgorithm(FirstMethod.First, points, tList);
            UseHeuristicAlgorithm(FirstMethod.Last, points, tList);
            UseHeuristicAlgorithm(FirstMethod.Random, points, tList);

            Console.WriteLine("Variant 2");

            points = new List<Point>
            {
                new Point(-7.02, 2.94), new Point(-5.77, 2.16), new Point(-8.17, -1.19), new Point(-2.42, -2.87),
                new Point(-4.49, 0.76), new Point(-4.54, -1.51), new Point(0.26, -5.71), new Point(-0.29, -7.21), new Point(1.74, -7.8),
                new Point(-1.84, -8.49), new Point(1.01, -9.44), new Point(-1.19, -5.76), new Point(7.0, 0.33), new Point(3.64, 0.05),
                new Point(5.97, 0.42), new Point(5.24, -1.99), new Point(5.39, -3.97), new Point(7.95, -3.17), new Point(7.05, -1.7),
                new Point(3.94, -2.25), new Point(-0.64, 7.88), new Point(-1.34, 9.17), new Point(-2.57, 9.17), new Point(-3.94, 7.74),
                new Point(-1.04, 5.06), new Point(-3.02, 5.52), new Point(-1.87, 7.7), new Point(7.7, 7.26), new Point(5.52, 8.36),
                new Point(4.52, 7.22), new Point(4.87, 5.72), new Point(7.15, 4.67), new Point(6.82, 7.1), new Point(6.15, 6.94),
                new Point(6.65, 9.4)
            };
            tList = new List<int> { 3, 4, 6, 7, 8, 10 };

            UseHeuristicAlgorithm(FirstMethod.First, points, tList);
            UseHeuristicAlgorithm(FirstMethod.Last, points, tList);
            UseHeuristicAlgorithm(FirstMethod.Random, points, tList);

            Console.WriteLine("Variant 3");

            points = new List<Point>
            {
                new Point(6.02, 7.03), new Point(-7.47, 7.91), new Point(-1.61, -6.19), new Point(-5.24, 7.06),
                new Point(-6.24, 6.67), new Point(-7.2, 3.88), new Point(-4.63, 3), new Point(6.76, 5.06),
                new Point(4.36, 7.21), new Point(4.36, 2.85), new Point(3.85, 4.55), new Point(5.55, 5.12),
                new Point(-1.63, -2.85), new Point(-2.01, -4.76), new Point(-4.72, -4.46), new Point(-3.06, -8.28),
                new Point(1.03, -6.03), new Point(-6.02, 4.27), new Point(6.36, 3.45), new Point(-3.74, -6.25),
                new Point(-1.1, -7.55), new Point(4.59, -4.15), new Point(-6.35, 1.94), new Point(-4.14, 4.84)
            };

            tList = new List<int> { 3, 4, 6, 7, 8, 10 };

            UseHeuristicAlgorithm(FirstMethod.First, points, tList);
            UseHeuristicAlgorithm(FirstMethod.Last, points, tList);
            UseHeuristicAlgorithm(FirstMethod.Random, points, tList);

            Console.ReadKey();
        }

        static void UseHeuristicAlgorithm(FirstMethod firstMethod, List<Point> points, List<int> tList)
        {
            HeuristicAlgorithm heuristicAlgorithm = new HeuristicAlgorithm(firstMethod, points);
            heuristicAlgorithm.T = tList;
            heuristicAlgorithm.RunHeuristicAlgorithm();
            Console.WriteLine("-------------------------------------------------------");
        }
    }
}