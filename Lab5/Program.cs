using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            IsoDataAlgorithm isoDataAlgorithm = new IsoDataAlgorithm();
            IList<Cluster> clusters;
            IList<Point> points;
            IList<Cluster> result;


            Console.WriteLine("1.1");
            Console.WriteLine("-------------------------------------------------------------------------");

            clusters = new List<Cluster>
            {
                new Cluster(new Point(0, 0))
            };
            points = new Point[]
            {
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2),
                new Point(4, 3),
                new Point(5, 3),
                new Point(4, 4),
                new Point(5, 4),
                new Point(6, 5)
            };
            result = isoDataAlgorithm.RunAlgorithm(clusters, points, 2, 1, 1, 4, 0, 4);

            Console.WriteLine("1.2");
            Console.WriteLine("-------------------------------------------------------------------------");

            clusters = new List<Cluster>
            {
                new Cluster(new Point(0, 0)),
                new Cluster(new Point(1, 1))
            };
            result = isoDataAlgorithm.RunAlgorithm(clusters, points, 3, 2, 0.8, 3, 2, 5);

            Console.WriteLine("1.3");
            Console.WriteLine("-------------------------------------------------------------------------");

            clusters = new List<Cluster>
            {
                new Cluster(new Point(0, 0)),
                new Cluster(new Point(4, 4)),
                new Cluster(new Point(6, 5))
            };
            result = isoDataAlgorithm.RunAlgorithm(clusters, points, 4, 2, 0.5, 2, 3, 6);

            Console.WriteLine("2.1");
            Console.WriteLine("-------------------------------------------------------------------------");
        }
    }
}
