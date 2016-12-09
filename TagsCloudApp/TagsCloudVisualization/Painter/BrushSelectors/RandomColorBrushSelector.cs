using System;
using System.Drawing;

namespace TagsCloudVisualization.Painter.BrushSelectors
{
    public class RandomColorBrushSelector : IBrushSelector
    {
        private readonly Random random = new Random();

        public Brush SelectBrush(string word, int count)
        {
            return new SolidBrush(ColorUtils.GetRandomColor(random));
        }
    }
}
