using Common.Models;
using System;
using System.Collections.Generic;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            BayesMethod bayesMethod = new BayesMethod();
            Cluster[] clasters;
            IEnumerable<(string, Matrix, Matrix)> result;

            Console.WriteLine("1st Variant:");
            clasters = new Cluster[]
            {
                new Cluster(new Point[]
                {
                    new Point(5, 6),
                    new Point(3, 2),
                    new Point(9, 4),
                    new Point(4, -1),
                    new Point(0, 5)
                }),
                new Cluster(new Point[]
                {
                    new Point(-3, -4),
                    new Point(-1, -2),
                    new Point(-4, 0),
                    new Point(0, -3),
                    new Point(-2, 2),
                    new Point(1, -3),
                    new Point(-3, 3)
                })
            };
            result = bayesMethod.FindDecisiveFunction(clasters);
            ShowResult(result);

            Console.WriteLine("2nd Variant:");

            clasters = new Cluster[]
            {
                new Cluster(new Point[]
                {
                    new Point(2, 4),
                    new Point(3, 2),
                    new Point(0, 5),
                    new Point(1, 4),
                    new Point(-1, 3),
                    new Point(1, 1),
                    new Point(2, 1),
                    new Point(1, 2),
                    new Point(-2, 5),
                    new Point(-6, -2),
                    new Point(3, 4),
                    new Point(0, 4),
                    new Point(4, 0),
                    new Point(0, 3)
                }),
                new Cluster(new Point[]
                {
                    new Point(-4, -2),
                    new Point(-1, -3),
                    new Point(-5, 0),
                    new Point(-3, 2),
                    new Point(-2, -4),
                    new Point(2, -5),
                    new Point(-2, 2),
                    new Point(-3, 3),
                    new Point(1, -5),
                    new Point(0, -3),
                    new Point(-2, 0)
                })
            };

            result = bayesMethod.FindDecisiveFunction(clasters);
            ShowResult(result);

            Console.WriteLine("3d Variant:");

            clasters = new Cluster[]
            {
                new Cluster(new Point[]
                {
                    new Point(-3, -4),
                    new Point(-2, -4),
                    new Point(2, -5),
                    new Point(1, -5),
                    new Point(0, -3)
                }),
                new Cluster(new Point[]
                {
                    new Point(-3.5, -2.8),
                    new Point(-3, 2),
                    new Point(-3, -3),
                    new Point(-2, 0)
                }),
                new Cluster(new Point[]
                {
                    new Point(2, 4),
                    new Point(0, 5),
                    new Point(1, 4),
                    new Point(-1, 3),
                    new Point(1, 1),
                    new Point(2, 1),
                    new Point(1, 2),
                    new Point(-2, 5),
                    new Point(6, -2),
                    new Point(3, 4),
                    new Point(-2, 2),
                    new Point(0, 4),
                    new Point(4, 0),
                    new Point(0, 3)
                })
            };

            result = bayesMethod.FindDecisiveFunction(clasters);
            ShowResult(result);

            Console.ReadKey();
        }

        static void ShowResult(IEnumerable<(string, Matrix, Matrix)> result)
        {
            int counter = 0;
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Item1} {item.Item2.Array[0, 0]}x1+({item.Item2.Array[1, 0]}x2)+({item.Item3.Array[0, 0]})");
            }
            Console.WriteLine();
        }
    }
}
