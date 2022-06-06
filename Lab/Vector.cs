using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    class Vector
    {
        public double x;
        public double y;

        public Vector (Point A, Point B)
        {
            this.x = B.x - A.x;
            this.y = B.y - A.y;
        }

        public Vector (double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public int CompareTo(object obj) 
        {
            Vector vector = (Vector)obj;
            if (this.x == 0 && this.y == 0)
                return -1;
            if (vector.x == 0 && vector.y == 0)
                return 1;
            try
            {
                if (this.y / this.x > vector.y / vector.x)
                    return 1;
                if (this.y / this.x < vector.y / vector.x)
                    return -1;
                else return 0;
            }
            catch (DivideByZeroException)
            {
                if (this.y > vector.y)
                    return 1;
                if (this.y < vector.y)
                    return -1;
                else return 0;
            }
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y);
        }

    }
}
