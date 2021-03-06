﻿using System.Collections.Generic;
using System.Linq;

namespace TagsSelectionTests
{
    internal abstract class WordsSelectorTests
    {
        protected static Dictionary<string, int> CreateSingleWordDict(string word)
        {
            IEnumerable<string> wordEnumerable = new[] { word };
            return wordEnumerable.ToDictionary(w => w, w => 1);
        }
    }
}
