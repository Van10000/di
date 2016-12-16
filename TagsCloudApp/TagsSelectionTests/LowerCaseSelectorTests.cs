using FluentAssertions;
using NUnit.Framework;
using TagsCloudApplication.WordsSelectors;

namespace TagsSelectionTests
{
    [TestFixture]
    class LowerCaseSelectorTests : WordsSelectorTests
    {
        [TestCase("low")]
        [TestCase("HIGH")]
        [TestCase("CaMeL")]
        [TestCase("ВеРбЛюД")]
        public void LowerCaseIsCorrectTest(string word)
        {
            var selector = new LowerCaseWordsSelector();
            var dict = CreateSingleWordDict(word);
            var lowerCase = word.ToLowerInvariant();

            var selected = selector.SelectWords(dict);

            selected.Count.Should().Be(1);
            selected.Should().ContainKey(lowerCase);
            selected[lowerCase].Should().Be(1);
        }
    }
}
