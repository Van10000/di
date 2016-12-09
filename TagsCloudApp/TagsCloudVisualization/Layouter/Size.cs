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

        public static Size operator /(Size a, int d)
        {
            return new Size(Math.Max(a.Width / d, 1), Math.Max(a.Height / d, 1));
        }
    }
}
