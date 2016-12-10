using System.Collections.Generic;

namespace TagsCloudVisualization.Layouter
{
    public interface ILayouter
    {
        IEnumerable<Rectangle> PutAllRectangles(IEnumerable<Size> sizes);
    }
}
