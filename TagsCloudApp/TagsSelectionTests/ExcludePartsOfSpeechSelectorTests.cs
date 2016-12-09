using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudApplication.WordsSelectors;
using FluentAssertions;
using NUnit.Framework;

namespace TagsSelectionTests
{
    [TestFixture]
    internal class ExcludePartsOfSpeechSelectorTests : WordsSelectorTests
    {
        [TestCase("i", ExpectedResult = true)]
        [TestCase("you", ExpectedResult = true)]
        [TestCase("he", ExpectedResult = true)]
        [TestCase("me", ExpectedResult = true)]
        [TestCase("it", ExpectedResult = true)]
        [TestCase("your", ExpectedResult = true)]
        [TestCase("their", ExpectedResult = true)]
        [TestCase("him", ExpectedResult = true)]
        [TestCase("hello", ExpectedResult = false)]
        public bool SingleWordIsPronounTest(string word)
        {
            var selector = new ExcludePartsOfSpeechSelector(ExcludePartsOfSpeechSelector.Pronoun);
            var wordDict = CreateSingleWordDict(word);

            wordDict = selector.SelectWords(wordDict);

            return wordDict.Count == 1;
        }
    }
}
