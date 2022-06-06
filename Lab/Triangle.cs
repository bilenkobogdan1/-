using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    class Triangle
    {
        private static double square(Point a, Point b, Point c)
        {
            double ab = lenght(a, b);
            double bc = lenght(b, c);
            double ac = lenght(a, c);
            double p = (ab + bc + ac) / 2;
            return Math.Sqrt(p * (p - ab) * (p - ac) * (p - bc));
        }

        private static double lenght(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }

        public static List<Point> triangle(List<Point> border)
        {
            double sq = 0;
            List<Point> res = new List<Point>();
            foreach (Point a in border)
                foreach (Point b in border)
                    foreach (Point c in border)
                        if (square(a, b, c) > sq)
                        {
                            sq = square(a, b, c);
                            res.Clear();
                            res.Add(a);
                            res.Add(b);
                            res.Add(c);
                            res.Add(a);
                        }
            return res;
        }
    }
}
