using System.Collections.Generic;
using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization.Painter.WordsPlacers
{
    public interface IWordsPlacer
    {
        int ImageWidth { get; }
        int ImageHeight { get; }
        WordPlaced[] GetWordsFormatted(Dictionary<string, int> wordsStatistics);
    }
}
