using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudApplication.WordsSelectors
{
    public class ComplexWordsSelector : IWordsSelector
    {
        private readonly IWordsSelector[] wordsSelectors;

        public ComplexWordsSelector(params IWordsSelector[] wordsSelectors)
        {
            this.wordsSelectors = wordsSelectors;
        }

        public Dictionary<string, int> SelectWords(Dictionary<string, int> wordsStatistics)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            // Don't like how linq looks like here. Loop is more obvious.
            foreach (var wordsSelector in wordsSelectors)
                wordsStatistics = wordsSelector.SelectWords(wordsStatistics);
            return wordsStatistics;
        }
    }
}
