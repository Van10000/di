using System.Collections.Generic;

namespace TagsCloudApplication.WordsSelectors
{
    public interface IWordsSelector
    {
        Dictionary<string, int> SelectWords(Dictionary<string, int> wordsStatistics);
    }
}
