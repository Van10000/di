using System.Collections.Generic;
using System.Text;
using TagsCloudApplication.TextSuppliers;
using TagsCloudApplication.Utils;

namespace TagsCloudApplication.WordsStatisticsSuppliers
{
    public class WordsStatisticsSupplier
    {
        private readonly ITextSupplier textSupplier;

        public WordsStatisticsSupplier(ITextSupplier textSupplier)
        {
            this.textSupplier = textSupplier;
        }

        public Dictionary<string, int> GetWordsStatistics()
        {
            var text = textSupplier.SupplyText();
            var words = new Dictionary<string, int>();
            for (var i = 0; i < text.Length; ++i)
                if (char.IsLetter(text[i]))
                {
                    var word = GetWord(text, i, out i);
                    words.AddIntValue(word, 1);
                }
            return words;
        }

        private string GetWord(string text, int currentPosition, out int nextPosition)
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
            return result.ToString();
        }
    }
}
