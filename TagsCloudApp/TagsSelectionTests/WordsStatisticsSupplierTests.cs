using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;
using TagsCloudApplication.TextSuppliers;
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
            var supplier = A.Fake<ITextSupplier>();
            A.CallTo(() => supplier.SupplyText()).Returns(text);

            var statistics = new WordsStatisticsSupplier(supplier).GetWordsStatistics();

            statistics.Keys.Should().BeEquivalentTo(words);
            statistics.Values.ShouldAllBeEquivalentTo(1);
        }
    }
}
