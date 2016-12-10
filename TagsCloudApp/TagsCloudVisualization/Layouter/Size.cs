using System;

namespace TagsCloudVisualization.Layouter
{
    public class Size
    {
        public readonly int Width;
        public readonly int Height;
        
        public int Area => Width * Height;

        public Size(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException(
                    $"Both width and height should be positive. " +
                    $"Your width:{width}, your height:{height}");
            Width = width;
            Height = height;
        }

        public static Size CreateSafe(int width, int height)
        {
            return new Size(Math.Max(width, 1), Math.Max(height, 1));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Size)obj);
        }
        
        protected bool Equals(Size other)
        {
            return Width == other.Width && Height == other.Height;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Width * 397) ^ Height;
            }
        }

        public static Size operator *(Size a, double d)
        {
            return CreateSafe(Round(a.Width * d), Round(a.Height * d));
        }

        public static Size operator /(Size a, int d)
        {
            return CreateSafe(a.Width / d, a.Height / d);
        }

        private static int Round(double val)
        {
            return (int)Math.Round(val);
        }
    }
}
