using System.Collections.Generic;
using Utils;

namespace TagsCloudVisualization.Painter
{
    public interface ITextDrawer
    {
        Result<None> WritePictureToFile(Dictionary<string, int> wordsStatistics, string filename);
    }
}
