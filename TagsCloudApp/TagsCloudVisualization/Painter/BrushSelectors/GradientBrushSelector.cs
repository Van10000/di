using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Painter.BrushSelectors
{
    public class GradientBrushSelector : StatisticsBrushSelector
    {
        private readonly Color mostFrequentWordColor;
        private readonly Color leastFrequentWordColor;
        private readonly int maximalWordOccurrenceNumber;

        public GradientBrushSelector(Dictionary<string, int> wordsStatistics, Color mostFrequentWordColor, Color leastFrequentWordColor) 
            : base(wordsStatistics)
        {
            this.mostFrequentWordColor = mostFrequentWordColor;
            this.leastFrequentWordColor = leastFrequentWordColor;
            maximalWordOccurrenceNumber = wordsStatistics.Values.Max();
        }

        public override Brush SelectBrush(string word, int count)
        {
            var frequencyCoefficient = count / (double)(maximalWordOccurrenceNumber + 1);
            return new SolidBrush(ColorUtils.GetInRatio(mostFrequentWordColor, leastFrequentWordColor, frequencyCoefficient));
        }
    }
}
