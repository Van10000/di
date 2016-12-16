using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudApplication.WordsSelectors
{
    public class PredicateWordsSelector : IWordsSelector
    {
        private readonly Predicate<string> predicate;

        public PredicateWordsSelector(Predicate<string> predicate)
        {
            this.predicate = predicate;
        }

        public Dictionary<string, int> SelectWords(Dictionary<string, int> wordsStatistics)
        {
            return wordsStatistics
                .Where(pair => predicate(pair.Key))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
