using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    class HeuristicAlgorithm
    {
        private readonly List<Point> points;
        private readonly List<Cluster> clasters = new List<Cluster>();
        private readonly Point firstZ;
        public IEnumerable<int> T = new List<int>();
        public HeuristicAlgorithm(FirstMethod firstMethod, IEnumerable<Point> points)
        {
            this.points = points.ToList();
            switch (firstMethod)
            {
                case FirstMethod.First:
                    firstZ = points.ElementAt(0);
                    break;
                case FirstMethod.Last:
                    firstZ = points.Last();
                    break;
                case FirstMethod.Random:
                    firstZ = points.ElementAt(new Random().Next(0, points.Count()));
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void RunHeuristicAlgorithm()
        {
            double d;
            Point xMinusZ;
            foreach (int number in T)
            {
                foreach (Point point in points)
                {
                    if (!clasters.Any())
                    {
                        clasters.Add(new Cluster(new List<Point>(), firstZ));
                        continue;
                    }
                    bool isInClaster = false;
                    foreach (Cluster claster in clasters)
                    {
                        xMinusZ = point - claster.Center;
                        d = Math.Sqrt(Math.Pow(xMinusZ.X, 2) + Math.Pow(xMinusZ.Y, 2));
                        if(d <= number)
                        {
                            claster.Points = claster.Points.Append(point);
                            isInClaster = true;
                            break;
                        }
                    }
                    if(!isInClaster)
                    {
                        clasters.Add(new Cluster(new List<Point>(), point));
                    }
                }
                foreach (Cluster claster in clasters)
                {
                    Console.Write($"Claster with a center in ({claster.Center.X}, {claster.Center.Y}) has points: ");
                    if (!claster.Points.Any())
                    {
                        Console.WriteLine("none");
                        continue;
                    }
                    foreach (Point point in claster.Points)
                    {
                        Console.Write($"({point.X}, {point.Y}) ");
                    }
                    Console.WriteLine();
                }
                Console.Write("\nz = ");
                foreach (var claster in clasters)
                {
                    Console.Write($"({claster.Center.X}, {claster.Center.Y}) ");
                }
                Console.WriteLine();
                Console.WriteLine($"Number of clusters: {clasters.Count}");
                Console.WriteLine();

                foreach (var claster in clasters)
                {
                    var averageDistance = GetAverageDistance(claster);
                    var disperse = FindDisperse(claster, averageDistance);
                    var minMax = FindMinMaxDistance(claster);

                    Console.WriteLine($"Average Distance = {averageDistance}," +
                        $" disperse = {disperse}," +
                        $" AverageSqrt = {Math.Sqrt(disperse)}," +
                        $" Min = {minMax.Item1}," +
                        $" Max = {minMax.Item2} ");

                    if(clasters.Any() && clasters.IndexOf(claster) < clasters.Count - 1)
                    {
                        Console.WriteLine($"Distance to the next Z is {GetDistance(claster.Center, clasters[clasters.IndexOf(claster) + 1].Center)}");
                    }

                    Console.WriteLine("-----------------------------------------------------");
                }
                Console.WriteLine();
                Console.WriteLine("=============================================================");
                clasters.Clear();
            }
        }

        private double GetAverageDistance(Cluster claster)
        {
            double sum = 0.0, distance;
            foreach (Point point in claster.Points)
            {
                distance = GetDistance(point, claster.Center);
                sum += distance;
            }

            return sum / claster.Points.Count();
        }

        private double FindDisperse(Cluster claster, double averageDistTo)
        {
            double sum = 0.0, disperse;
            foreach (Point point in claster.Points)
            {
                sum += Math.Pow(GetDistance(point, claster.Center) - averageDistTo, 2);
            }
            disperse = sum / claster.Points.Count();
            return disperse;
        }

        private (double, double) FindMinMaxDistance(Cluster claster)
        {
            double min = claster.Points.Any() ? GetDistance(claster.Points.ElementAt(0), claster.Center) : 0;
            double max = claster.Points.Any() ? GetDistance(claster.Points.ElementAt(0), claster.Center) : 0;
            double distance;

            foreach (Point point in points)
            {
                distance = GetDistance(point, claster.Center);
                if(distance > max)
                {
                    max = distance;
                }

                if(distance < min)
                {
                    min = distance;
                }
            }

            return (min, max);
        }

        private double GetDistance(Point from, Point to)
        {
            return Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2));
        }
    }
}
