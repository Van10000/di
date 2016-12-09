﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudApplication.Utils
{
    internal static class IntValueDictionaryExtentions
    {
        public static void AddIntValue<TKey>(this Dictionary<TKey, int> dict, TKey key, int toAdd)
        {
            if (dict.ContainsKey(key))
                dict[key] += toAdd;
            else
                dict[key] = toAdd;
        }
    }
}
