using System.Collections.Generic;

namespace TagsCloudVisualization.Painter
{
    public interface ITextDrawer
    {
        void WritePictureToFile(Dictionary<string, int> wordsStatistics, string filename);
    }
}
