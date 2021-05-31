using System.Collections.Generic;

namespace Common.Models
{
    public class Claster
    {
        public IEnumerable<Point> Points { get; set; }
        public Point Center { get; set; }

        public Claster(IEnumerable<Point> points, Point center)
        {
            Points = points;
            Center = center;
        }

        public Claster(IEnumerable<Point> points)
        {
            Points = points;
        }
    }
}
