using System.Collections.Generic;

namespace Common.Models
{
    public class Cluster
    {
        static int id = 0;
        public int Id { get; private set; }
        public IList<Point> Points { get; set; }
        public Point Center { get; set; }

        public Cluster(IList<Point> points, Point center)
        {
            Points = points;
            Center = center;
        }

        public Cluster(IList<Point> points)
        {
            Points = points;
        }

        public Cluster(Point center)
        {
            Id = id++;
            Center = center;
            Points = new List<Point>();
        }
    }
}
