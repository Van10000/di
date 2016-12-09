using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public static class RectangleExtentions
    {
        public static System.Drawing.Rectangle ToDrawingRectangle(this Layouter.Rectangle rect)
        {
            return new System.Drawing.Rectangle(
                rect.LeftDown.X,
                rect.LeftDown.Y,
                rect.Size.Width,
                rect.Size.Height);
        }
    }
}
