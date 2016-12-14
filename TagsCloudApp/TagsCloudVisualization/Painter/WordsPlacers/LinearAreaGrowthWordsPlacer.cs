using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Layouter;
using Point = TagsCloudVisualization.Layouter.Point;
using Rectangle = TagsCloudVisualization.Layouter.Rectangle;
using Size = TagsCloudVisualization.Layouter.Size;

namespace TagsCloudVisualization.Painter.WordsPlacers
{
    public class LinearAreaGrowthWordsPlacer : IWordsPlacer
    {
        public readonly Size ImageSize;
        private readonly ILayouter layouter;

        public Point Center => new Point(ImageWidth / 2, ImageHeight / 2);
        public int ImageWidth => ImageSize.Width;
        public int ImageHeight => ImageSize.Height;
        
        public LinearAreaGrowthWordsPlacer(ILayouter layouter, Size imageSize)
        {
            ImageSize = imageSize;
            this.layouter = layouter;
        }

        public WordPlaced[] GetWordsFormatted(Dictionary<string, int> wordsStatistics, Dictionary<string, SizeF> wordsRelativeSizes)
        {
            var sizes = wordsStatistics
                .OrderByDescending(pair => pair.Value)
                .Select(pair => wordsRelativeSizes[pair.Key].GetMultiplied(pair.Value))
                .Select(size => size.Ceiling());
            
            var rectangles = layouter
                .PutAllRectangles(sizes)
                .Select(rect => rect.GetShifted(new Size(Center.X, Center.Y)));
            var scaled = ScaleToFitPrecisely(rectangles);
            return wordsStatistics.Keys
                .Zip(scaled, (word, rect) => new WordPlaced(word, rect))
                .ToArray();
        }

        private IEnumerable<Rectangle> ScaleToFitPrecisely(IEnumerable<Rectangle> rectangles)
        {
            var rectList = rectangles.ToList();
            var minX = rectList.Min(rect => rect.LeftDown.X);
            var minY = rectList.Min(rect => rect.LeftDown.Y);
            var maxX = rectList.Max(rect => rect.LeftDown.X + rect.Size.Width);
            var maxY = rectList.Max(rect => rect.LeftDown.Y + rect.Size.Height);
            var maxXScale = Math.Min(GetMaxScale(minX, ImageWidth), GetMaxScale(maxX, ImageWidth));
            var maxYScale = Math.Min(GetMaxScale(minY, ImageHeight), GetMaxScale(maxY, ImageHeight));
            var scaleCoeff = Math.Min(maxXScale, maxYScale);
            return rectList.Select(rect => rect.GetApproximatelyScaled(Center, scaleCoeff));
        }

        private static double GetMaxScale(int coord, int maxCoord)
        {
            var center = maxCoord / 2.0;
            var diff = Math.Abs(center - coord);
            return center / diff;
        }
    }
}
