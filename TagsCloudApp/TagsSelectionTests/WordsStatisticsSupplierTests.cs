using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudApplication.WordsStatisticsSuppliers;

namespace TagsSelectionTests
{
    [TestFixture]
    internal class WordsStatisticsSupplierTests
    {
        [TestCase("", new string[0])]
        [TestCase("Привет, мир!", new [] {"Привет", "мир"})]
        [TestCase("Здесь, будет,-очень!!!МнОгО;   разных        ненужных ~% символов", 
            new [] {"Здесь", "будет", "очень", "МнОгО", "разных", "ненужных", "символов"})]
        [TestCase("Works regardless the language.", new [] {"Works", "regardless", "the", "language"})]
        public void OneOfEveryWordTest(string text, string[] words)
        {
            var statistics = new WordsStatisticsSupplier(text).GetWordsStatistics();

            statistics.Keys.Should().BeEquivalentTo(words);
            statistics.Values.ShouldAllBeEquivalentTo(1);
        }
    }
}
