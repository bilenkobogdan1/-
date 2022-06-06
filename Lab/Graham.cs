using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lab
{
    class Graham
    {
        public Point initial;
        public List<Point> initial_points;
        public List<Point> points;
        public List<Point> border;
        private List<Point> triple;

        public Graham(List<Point> points)
        {
            this.points = points;
            this.border = new List<Point>();
            this.triple = new List<Point>();

            if (points.Count > 2)
                main();
            else
                MessageBox.Show("There're less than 3 points");
        }
        /// <summary>
        /// Finds point with minimun value of X
        /// </summary>
        public void find_initial_point()
        {
            double x_min = points.Min(p => p.x);
            double y_min = points.Where(p => p.x == x_min).Min(_p => _p.y);
            initial = new Point { x = x_min, y = y_min };
        }
        /// <summary>
        /// Sort points due to treir angle witin initial point.
        /// It shoul be used only when initial point has been found.
        /// </summary>
        public void sort()
        {
            points.Sort((a, b) =>
            {
                Vector A = new Vector(initial, a);
                Vector B = new Vector(initial, b);
                return A.CompareTo(B);
            });
            initial_points = clone(points);
        }
    
        public void set_triple(Point a, Point b, Point c)
        {
            triple.Clear();
            triple.Add(a);
            triple.Add(b);
            triple.Add(c);
        }
  
        public void main()
        {
            find_initial_point();
            sort();

            Point a = points[0];
            Point b = points[1];
            Point c = points[2];

            set_triple(a, b, c);

            border.Add(a);
            border.Add(b);

            for (int index = 2; index < points.Count + 2;)
            {
                index = step(index);
            }
            border.Add(initial);
            points = initial_points;
        }
    
        private int step(int index)
        {
            Point a = triple[0];
            Point b = triple[1];
            Point c = triple[2];

            if (isRight(b, a, c))
            {
                points.Remove(b);
                if (border.Count > 1)
                {
                    border.Remove(a);
                    triple[1] = a;
                    triple[0] = border.Last();
                    index--;
                }
                else
                {
                    triple[1] = c;
                    triple[2] = points[index % points.Count];
                }
            }
            else
            {
                index++;
                border.Add(b);
                triple[0] = b;
                triple[1] = c;
                triple[2] = points[index % points.Count];
            }

            return index;
        }
      
        private bool isRight(Point a, Point b, Point c)
        {
            return ((a.x - c.x) * (b.y - c.y)) - ((b.x - c.x) * (a.y - c.y)) > 0;
        }
        
        private List<Point> clone (List<Point> list)
        {
            List<Point> result = new List<Point>();
            foreach (Point obj in list)
                result.Add(obj);
            return result;
        }
    }
}
