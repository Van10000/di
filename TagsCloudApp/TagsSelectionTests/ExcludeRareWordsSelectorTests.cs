using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudApplication.WordsSelectors;

namespace TagsSelectionTests
{
    [TestFixture]
    class ExcludeRareWordsSelectorTests
    {
        [Test]
        public void SimpleExcludeRareWordsTest()
        {
            var wordsStatistics = new Dictionary<string, int>
            {
                ["word"] = 5,
                ["something"] = 3,
                ["rare"] = 1
            };
            var selector = new ExcludeRareWordsSelector(2);
            wordsStatistics = selector.SelectWords(wordsStatistics);
            wordsStatistics.Should().ContainKeys("word", "something");
            wordsStatistics.Should().NotContainKey("rare");
        }

        [Test]
        public void DoNotExcludeIfLowCountTest()
        {
            var wordsStatistics = new Dictionary<string, int>
            {
                ["word"] = 5,
                ["something"] = 3,
                ["rare"] = 1
            };
            var selector = new ExcludeRareWordsSelector(3);
            wordsStatistics = selector.SelectWords(wordsStatistics);
            wordsStatistics.Should().ContainKeys("word", "something", "rare");
        }       
    }
}
