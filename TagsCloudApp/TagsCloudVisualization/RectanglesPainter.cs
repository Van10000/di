using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Painter.BrushSelectors;
using Point = TagsCloudVisualization.Layouter.Point;
using Rectangle = TagsCloudVisualization.Layouter.Rectangle;

namespace TagsCloudVisualization
{
    public static class RectanglesPainter
    {
        private const double MinimalRelativeMargin = 0.2;
        private const int MinimalAbsoluteMargin = 5;

        public static Bitmap GetPicture(IList<Rectangle> rectangles)
        {
            var width = FindIntervalWithMargin(rectangles, p => p.X);
            var height = FindIntervalWithMargin(rectangles, p => p.Y);

            var center = new Point(
                (int) rectangles.Average(rect => rect.GetApproximateCenter().X),
                (int) rectangles.Average(rect => rect.GetApproximateCenter().Y));

            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            var shift = new Point(width / 2, height / 2) - center;
            graphics.FillRegion(Brushes.White, new Region(new System.Drawing.Rectangle(0, 0, width, height)));
            var rand = new Random();
            foreach (var rect in rectangles)
            {
                var rectToPaint = rect.GetShifted(shift).ToDrawingRectangle();
                var color = ColorUtils.GetRandomColor(rand);
                graphics.FillRectangle(new SolidBrush(color), rectToPaint);
            }
            return bitmap;
        }

        private static int FindIntervalWithMargin(IList<Rectangle> rectangles, Func<Point, int> keySelector)
        {
            var interval = rectangles.GetAllPoints().Max(keySelector) - rectangles.GetAllPoints().Min(keySelector);
            var withRelativeMargin = (int) (interval * (1 + MinimalRelativeMargin));
            var withAbloluteMargin = interval + MinimalAbsoluteMargin;
            return Math.Max(withRelativeMargin, withAbloluteMargin);
        }
    }
}
