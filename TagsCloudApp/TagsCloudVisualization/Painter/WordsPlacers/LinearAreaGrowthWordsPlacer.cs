using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Layouter;
using Point = TagsCloudVisualization.Layouter.Point;
using Rectangle = TagsCloudVisualization.Layouter.Rectangle;

namespace TagsCloudVisualization.Painter.WordsPlacers
{
    public class LinearAreaGrowthWordsPlacer : IWordsPlacer
    {
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        public Point Center => new Point(ImageWidth / 2, ImageHeight / 2);

        private CircularCloudLayouter layouter; // it's not abstraction because here we need this specific layouter

        public LinearAreaGrowthWordsPlacer()
        {
            ImageWidth = 500;
            ImageHeight = 500;
        }

        public WordPlaced[] GetWordsFormatted(Dictionary<string, int> wordsStatistics, Dictionary<string, SizeF> wordsRelativeSizes)
        {
            layouter = new CircularCloudLayouter(Center);
            //var totalLettersNumber = wordsStatistics.Sum(pair => pair.Key.Length * pair.Value);
            //var maximalWordLength = wordsStatistics.Keys.Max(str => str.Length);
            //var letterWidth = FindMaximalAppropriateLetterWidth(totalLettersNumber, maximalWordLength);
            //var letterHeight = letterWidth * WidthToHeightLetterRatio;
            var sizes = wordsStatistics
                .Select(pair => wordsRelativeSizes[pair.Key].GetMultiplied(pair.Value))
                .Select(size => size.CeilToCustom());
            
            var rectangles = layouter.PutAllRectangles(sizes);
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
            scaleCoeff *= 0.95; // to stay little margins at the edges
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
