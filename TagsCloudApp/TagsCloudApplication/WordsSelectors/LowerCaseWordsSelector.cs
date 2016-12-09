using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudApplication.Utils;

namespace TagsCloudApplication.WordsSelectors
{
    public class LowerCaseWordsSelector : IWordsSelector
    {
        public Dictionary<string, int> SelectWords(Dictionary<string, int> wordsStatistics)
        {
            var selected = new Dictionary<string, int>();
            foreach (var pair in wordsStatistics)
            {
                var word = pair.Key;
                var count = pair.Value;
                var lowerWord = word.ToLowerInvariant();
                selected.AddIntValue(lowerWord, count);
            }
            return selected;
        }
    }
}
