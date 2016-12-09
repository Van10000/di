using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TagsCloudVisualization.Layouter
{
    public class CircularCloudLayouter : ILayouter
    {
        private readonly Point center;
        private readonly List<Rectangle> previousRectangles = new List<Rectangle>();

        public ReadOnlyCollection<Rectangle> PreviousRectangles => previousRectangles.AsReadOnly();

        public CircularCloudLayouter(Point center)
        {
            this.center = center;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (previousRectangles.Count == 0)
                return AddRectangle(new Rectangle(center - rectangleSize / 2, rectangleSize));
            var resultRect = previousRectangles
                .GetAllPoints()
                .Select(point => PutRectangleAtPoint(point, rectangleSize))
                .Where(newRect => newRect != null)
                .OrderBy(newRect => newRect
                                    .GetPoints()
                                    .Min(point => center.Distance(point)))
                .FirstOrDefault();
            return resultRect != null ? AddRectangle(resultRect) : null;
        }

        private Rectangle AddRectangle(Rectangle rectangle)
        {
            previousRectangles.Add(rectangle);
            return rectangle;
        }
        
        private bool CanBeAdded(Rectangle rect)
        {
            return !previousRectangles.Any(rect.IntersectsWith);
        }
        
        private Rectangle PutRectangleAtPoint(Point point, Size rectangleSize)
        {
            for (var i = 0; i <= 1; ++i)
                for (var j = 0; j <= 1; ++j)
                {
                    var curX = point.X - rectangleSize.Width * i;
                    var curY = point.Y - rectangleSize.Height * j;
                    var curRect = new Rectangle(new Point(curX, curY), rectangleSize);
                    if (CanBeAdded(curRect))
                        return curRect;
                }
            return null;
        }

        public IEnumerable<Rectangle> PutAllRectangles(IEnumerable<Size> sizes)
        {
            foreach (var size in sizes)
                PutNextRectangle(size);
            return PreviousRectangles;
        }
    }
}
