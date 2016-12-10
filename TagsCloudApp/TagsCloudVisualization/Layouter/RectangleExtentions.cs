using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Layouter
{
    public static class RectangleExtentions
    {
        public static IEnumerable<Point> GetAllPoints(this IEnumerable<Rectangle> rectangles)
            => rectangles.SelectMany(rect => rect.GetPoints());

        public static Rectangle GetApproximatelyScaled(this Rectangle rect, Point center, double scaleCoeff)
        {
            var diffXScaled = (rect.LeftDown.X - center.X) * scaleCoeff;
            var diffYScaled = (rect.LeftDown.Y - center.Y) * scaleCoeff;
            var newLeftDown = new Point((int) (center.X + diffXScaled), (int) (center.Y + diffYScaled));
            var newSize = rect.Size * scaleCoeff;
            return new Rectangle(newLeftDown, newSize);
        }
    }
}
