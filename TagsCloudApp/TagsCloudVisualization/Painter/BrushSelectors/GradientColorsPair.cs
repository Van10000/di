using System.Drawing;

namespace TagsCloudVisualization.Painter.BrushSelectors
{
    public class GradientColorsPair
    {
        public readonly Color LeastFrequent;
        public readonly Color MostFrequent;

        public GradientColorsPair(Color mostFrequent, Color leastFrequent)
        {
            LeastFrequent = leastFrequent;
            MostFrequent = mostFrequent;
        }
    }
}
