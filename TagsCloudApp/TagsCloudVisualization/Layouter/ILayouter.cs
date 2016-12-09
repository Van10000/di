using System.Collections.Generic;

namespace TagsCloudVisualization.Layouter
{
    interface ILayouter
    {
        IEnumerable<Rectangle> PutAllRectangles(IEnumerable<Size> sizes);
    }
}
