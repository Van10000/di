using System.Collections.Generic;
using System.Linq;

namespace TagsCloudApplication.WordsSelectors
{
    public class ExcludeRareWordsSelector : IWordsSelector
    {
        private readonly int maximalAllowedWordsNumber;

        public ExcludeRareWordsSelector(int maximalAllowedWordsNumber)
        {
            this.maximalAllowedWordsNumber = maximalAllowedWordsNumber;
        }

        public Dictionary<string, int> SelectWords(Dictionary<string, int> wordsStatistics)
        {
            return wordsStatistics
                .OrderByDescending(pair => pair.Value)
                .Take(maximalAllowedWordsNumber)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
