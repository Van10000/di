using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
