using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Layouter;
using Size = TagsCloudVisualization.Layouter.Size;

namespace TagsCloudVisualization
{
    public static class DrawingExtentions
    {
        public static System.Drawing.Rectangle ToDrawingRectangle(this Layouter.Rectangle rect)
        {
            return new System.Drawing.Rectangle(
                rect.LeftDown.X,
                rect.LeftDown.Y,
                rect.Size.Width,
                rect.Size.Height);
        }

        public static System.Drawing.SizeF GetMultiplied(this SizeF size, float multiplier) 
            => new SizeF(size.Width * multiplier, size.Height * multiplier);

        public static Size CeilToCustom(this SizeF size) => new Size(Ceil(size.Width), Ceil(size.Height));

        public static bool IsInside(this SizeF size, Size appropriateSize)
        {
            return size.Height < appropriateSize.Height && size.Width < appropriateSize.Width;
        }

        private static int Ceil(float val) => (int)Math.Ceiling(val);
    }
}
