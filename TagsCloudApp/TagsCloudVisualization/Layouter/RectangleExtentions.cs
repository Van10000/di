using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Layouter
{
    public static class RectangleExtentions
    {
        public static IEnumerable<Point> GetAllPoints(this IEnumerable<Rectangle> rectangles)
            => rectangles.SelectMany(rect => rect.GetPoints());
    }
}
