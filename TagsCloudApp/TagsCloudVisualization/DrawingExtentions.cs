using System;
using System.Drawing;
using Size = TagsCloudVisualization.Layouter.Size;

namespace TagsCloudVisualization
{
    internal static class DrawingExtentions
    {
        public static System.Drawing.Rectangle ToDrawingRectangle(this Layouter.Rectangle rect)
        {
            return new System.Drawing.Rectangle(
                rect.LeftDown.X,
                rect.LeftDown.Y,
                rect.Size.Width,
                rect.Size.Height);
        }

        public static SizeF GetMultiplied(this SizeF size, float multiplier) 
            => new SizeF(size.Width * multiplier, size.Height * multiplier);

        public static Size Ceil(this SizeF size) => new Size(Ceil(size.Width), Ceil(size.Height));

        public static bool IsInside(this SizeF size, Size appropriateSize)
        {
            return size.Height < appropriateSize.Height && size.Width < appropriateSize.Width;
        }

        private static int Ceil(float val) => (int)Math.Ceiling(val);
    }
}
