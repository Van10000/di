using System.Collections.Generic;
using System.Linq;

namespace TagsCloudApplication.WordsSelectors
{
    public class ExcludeWordsSelector : PredicateWordsSelector
    {
        public ExcludeWordsSelector(params string[] wordsToExclude)
            : base(word => !ShouldExclude(new HashSet<string>(wordsToExclude), word))
        {
        }

        public static bool ShouldExclude(IEnumerable<string> wordsToExclude, string word)
        {
            return wordsToExclude.Contains(word);
        }
    }
}
