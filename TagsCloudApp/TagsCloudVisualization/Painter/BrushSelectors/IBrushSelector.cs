using System.Drawing;

namespace TagsCloudVisualization.Painter.BrushSelectors
{
    public interface IBrushSelector
    {
        Brush SelectBrush(string word, int count);
    }
}
