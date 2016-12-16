namespace TagsCloudApplication.WordsSelectors
{
    public class ExcludeLowLengthWordsSelector : PredicateWordsSelector
    {
        public ExcludeLowLengthWordsSelector(int lowestPossibleLength) 
            : base(word => WordIsHighLength(word, lowestPossibleLength))
        {
        }

        public static bool WordIsHighLength(string word, int lowestPossibleLength)
        {
            return word.Length >= lowestPossibleLength;
        }
    }
}
