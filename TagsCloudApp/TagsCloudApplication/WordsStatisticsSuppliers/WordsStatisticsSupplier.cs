using System;
using System.Collections.Generic;
using System.Text;
using TagsCloudApplication.TextSuppliers;
using TagsCloudApplication.Utils;
using System.Linq;
using Utils;


namespace TagsCloudApplication.WordsStatisticsSuppliers
{
    public class WordsStatisticsSupplier
    {
        private readonly ITextSupplier textSupplier;

        public WordsStatisticsSupplier(ITextSupplier textSupplier)
        {
            this.textSupplier = textSupplier;
        }

        public Result<Dictionary<string, int>> GetWordsStatistics()
        {
            return textSupplier
                .SupplyText()
                .Then(GetWordsStatistics);
        }

        private static Dictionary<string, int> GetWordsStatistics(string text)
        {
            var words = new Dictionary<string, int>();
            for (var i = 0; i < text.Length; ++i)
                if (char.IsLetter(text[i]))
                {
                    var word = GetWord(text, i, out i);
                    if (word != null)
                        words.AddIntValue(word, 1);
                }
            return words;
        }

        private static string GetWord(string text, int currentPosition, out int nextPosition)
        {
            var result = new StringBuilder();
            nextPosition = text.Length;
            for (int i = currentPosition; i < text.Length; ++i)
                if (char.IsLetter(text[i]))
                    result.Append(text[i]);
                else
                {
                    nextPosition = i;
                    break;
                }
            //var start = Math.Max(currentPosition - 2, 0);
            //var finish = Math.Min(nextPosition + 2, text.Length);
            //var context = text.Substring(start, finish - start + 1); // for debug
            if (IsAnySymbolAtPosition(text, currentPosition - 1, "'’`") || IsAnySymbolAtPosition(text, nextPosition, "'’`"))
                return null;
            return result.ToString();
        }

        private static bool IsAnySymbolAtPosition(string str, int pos, IEnumerable<char> symbols)
        {
            return
                pos >= 0 &&
                pos < str.Length &&
                symbols.Contains(str[pos]);
        }
    }
}
