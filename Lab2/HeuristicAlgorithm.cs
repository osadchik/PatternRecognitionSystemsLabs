﻿using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    class HeuristicAlgorithm
    {
        private readonly List<Point> points;
        private readonly List<Claster> clasters = new List<Claster>();
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
                        clasters.Add(new Claster(new List<Point>(), firstZ));
                        continue;
                    }
                    bool isInClaster = false;
                    foreach (Claster claster in clasters)
                    {
                        xMinusZ = point - claster.Z;
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
                        clasters.Add(new Claster(new List<Point>(), point));
                    }
                }
                foreach (Claster claster in clasters)
                {
                    foreach (Point point in claster.Points)
                    {
                        Console.Write($"({point.X}, {point.Y}) ");
                    }
                    Console.WriteLine();
                }
                Console.Write("z = ");
                foreach (var claster in clasters)
                {
                    Console.Write($"({claster.Z.X}, {claster.Z.Y}) ");
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
                        Console.WriteLine($"Distance to the next Z is {GetDistance(claster.Z, clasters[clasters.IndexOf(claster) + 1].Z)}");
                    }

                    Console.WriteLine("-----------------------------------------------------");
                }
                Console.WriteLine();
                Console.WriteLine("=============================================================");
                clasters.Clear();
            }
        }

        private double GetAverageDistance(Claster claster)
        {
            double sum = 0.0, distance;
            foreach (Point point in claster.Points)
            {
                distance = GetDistance(point, claster.Z);
                sum += distance;
            }

            return sum / claster.Points.Count();
        }

        private double FindDisperse(Claster claster, double averageDistTo)
        {
            double sum = 0.0, disperse;
            foreach (Point point in claster.Points)
            {
                sum += Math.Pow(GetDistance(point, claster.Z) - averageDistTo, 2);
            }
            disperse = sum / claster.Points.Count();
            return disperse;
        }

        private (double, double) FindMinMaxDistance(Claster claster)
        {
            double min = claster.Points.Any() ? GetDistance(claster.Points.ElementAt(0), claster.Z) : 0;
            double max = claster.Points.Any() ? GetDistance(claster.Points.ElementAt(0), claster.Z) : 0;
            double distance;

            foreach (Point point in points)
            {
                distance = GetDistance(point, claster.Z);
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
