using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Painter.BrushSelectors
{
    public class GradientBrushSelector : IBrushSelector
    {
        private readonly Color mostFrequentWordColor;
        private readonly Color leastFrequentWordColor;
        private readonly List<int> sortedWordsCounts;

        public GradientBrushSelector(IEnumerable<int> wordsCounts, GradientColorsPair colorsPair)
            : this(wordsCounts, colorsPair.MostFrequent, colorsPair.LeastFrequent)
        { }

        private GradientBrushSelector(IEnumerable<int> wordsCounts, Color mostFrequentWordColor, Color leastFrequentWordColor)
        {
            this.mostFrequentWordColor = mostFrequentWordColor;
            this.leastFrequentWordColor = leastFrequentWordColor;
            sortedWordsCounts = wordsCounts.OrderBy(x => x).ToList();
        }

        public Brush SelectBrush(string word, int count)
        {
            var numberOfElementsBefore = sortedWordsCounts.FindFirstBiggerOrEqualIndex(count);
            var frequencyCoefficient = numberOfElementsBefore / (double)sortedWordsCounts.Count;
            frequencyCoefficient = Math.Pow(frequencyCoefficient, Math.Pow(Math.Log(sortedWordsCounts.Count), 1.5));
            return new SolidBrush(ColorUtils.GetInRatio(mostFrequentWordColor, leastFrequentWordColor, frequencyCoefficient));
        }
    }
}
