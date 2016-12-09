using System.Collections.Generic;
using System.Text;
using TagsCloudApplication.Utils;

namespace TagsCloudApplication.WordsStatisticsSuppliers
{
    public class WordsStatisticsSupplier
    {
        public readonly string Text;

        public WordsStatisticsSupplier(string text)
        {
            this.Text = text;
        }

        public Dictionary<string, int> GetWordsStatistics()
        {
            var words = new Dictionary<string, int>();
            for (var i = 0; i < Text.Length; ++i)
                if (char.IsLetter(Text[i]))
                {
                    var word = GetWord(i, out i);
                    words.AddIntValue(word, 1);
                }
            return words;
        }

        private string GetWord(int currentPosition, out int nextPosition)
        {
            var result = new StringBuilder();
            nextPosition = Text.Length;
            for (int i = currentPosition; i < Text.Length; ++i)
                if (char.IsLetter(Text[i]))
                    result.Append(Text[i]);
                else
                {
                    nextPosition = i;
                    break;
                }
            return result.ToString();
        }
    }
}
