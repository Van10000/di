using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization.Painter.WordsPlacers
{
    public class LinearAreaGrowthWordsPlacer : IWordsPlacer
    {
        private const double WidthToHeightLetterRatio = 3;

        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        private CircularCloudLayouter layouter; // it's not abstraction because here we need this specific layouter

        public LinearAreaGrowthWordsPlacer()
        {
            ImageWidth = 500;
            ImageHeight = 500;
        }

        public WordPlaced[] GetWordsFormatted(Dictionary<string, int> wordsStatistics)
        {
            layouter = new CircularCloudLayouter(new Layouter.Point(ImageWidth / 2, ImageHeight / 2));
            var totalLettersNumber = wordsStatistics.Sum(pair => pair.Key.Length * pair.Value);
            var maximalWordLength = wordsStatistics.Keys.Max(str => str.Length);
            var letterWidth = FindMaximalAppropriateLetterWidth(totalLettersNumber, maximalWordLength);
            var letterHeight = letterWidth * WidthToHeightLetterRatio;
            var sizes = wordsStatistics.Select(pair => new Size((int)(pair.Key.Length * letterWidth * pair.Value), (int)letterHeight));
            var rectangles = layouter.PutAllRectangles(sizes); // TODO: we can scale them in order to fit the size precisely
            return wordsStatistics.Keys
                .Zip(rectangles, (word, rect) => new WordPlaced(word, rect))
                .ToArray();
        }

        public double FindMaximalAppropriateLetterWidth(int totalLettersNumber, int maximalWordLength)
        {
            double minLetterWidth = 0;
            double maxLetterWidth = Math.Max(ImageHeight, ImageWidth);
            while (maxLetterWidth - minLetterWidth > 1e-3)
            {
                double letterWidth = (maxLetterWidth + minLetterWidth) / 2;
                double totalWidth = totalLettersNumber * letterWidth;
                double totalHeight = totalLettersNumber * letterWidth * WidthToHeightLetterRatio;
                double totalArea = totalWidth * totalHeight;
                double approximateRadius = Math.Sqrt(totalArea / Math.PI);
                double errorWidth = maximalWordLength * letterWidth;
                double errorHeight = letterWidth * WidthToHeightLetterRatio;
                double minimalPossibleWidth = approximateRadius * 2 + errorWidth;
                double minimalPossibleHeight = approximateRadius * 2 + errorHeight;
                if (minimalPossibleHeight < ImageHeight && minimalPossibleWidth < ImageWidth)
                    minLetterWidth = letterWidth;
                else
                    maxLetterWidth = letterWidth;
            }
            return minLetterWidth;
        }
    }
}
