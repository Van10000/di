using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudApplication.WordsSelectors
{
    public class ExcludeLowLengthWordsSelector : PredicateWordsSelector
    {
        public ExcludeLowLengthWordsSelector() : base(WordIsHighLength)
        {
        }

        public static bool WordIsHighLength(string word)
        {
            return word.Length > 3;
        }
    }
}
