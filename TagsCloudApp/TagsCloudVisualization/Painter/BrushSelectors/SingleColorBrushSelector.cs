using System.Drawing;

namespace TagsCloudVisualization.Painter.BrushSelectors
{
    public class SingleColorBrushSelector : IBrushSelector
    {
        private readonly Color color;

        public SingleColorBrushSelector(Color color)
        {
            this.color = color;
        }

        public Brush SelectBrush(string word, int count)
        {
            return new SolidBrush(color);
        }
    }
}
