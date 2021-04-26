using System.Collections.Generic;

namespace Lab2.Models
{
    class Claster
    {
        public IEnumerable<Point> Points { get; set; }
        public Point Z { get; set; }

        public Claster(IEnumerable<Point> points, Point z)
        {
            Points = points;
            Z = z;
        }
    }
}
