using System.Collections.Generic;
using TagsCloudApplication.WordsSelectors;
using NUnit.Framework;

namespace TagsSelectionTests
{
    [TestFixture]
    internal class ExcludeWordsSelectorTests : WordsSelectorTests
    {
        [TestCase(new string[0], "a", ExpectedResult = false)]
        [TestCase(new [] { "a"}, "a", ExpectedResult = true)]
        [TestCase(new[] { "first", "second"}, "second", ExpectedResult = true)]
        [TestCase(new[] { "раз", "два"}, "first", ExpectedResult = false)]
        public bool ExcludeWordTest(string[] wordsToExclude, string word)
        {
            return ExcludeWordsSelector.ShouldExclude(wordsToExclude, word);
        }
    }
}
