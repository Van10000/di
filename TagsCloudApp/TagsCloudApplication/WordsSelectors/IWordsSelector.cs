using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudApplication.WordsSelectors
{
    public interface IWordsSelector
    {
        Dictionary<string, int> SelectWords(Dictionary<string, int> wordsStatistics);
    }
}
