using System;

namespace TagsCloudVisualization.Layouter
{
    public class Point
    {
        public readonly int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public double Distance(Point point)
        {
            var xProj = point.X - X;
            var yProj = point.Y - Y;
            return Math.Sqrt(xProj * xProj + yProj * yProj);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point) obj);
        }

        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static Size operator -(Point a, Point b)
        {
            return new Size(a.X - b.X, a.Y - b.Y);
        }

        public static Point operator +(Point a, Size b)
        {
            return new Point(a.X + b.Width, a.Y + b.Height);
        }

        public static Point operator -(Point a, Size b)
        {
            return new Point(a.X - b.Width, a.Y - b.Height);
        }
    }
}
