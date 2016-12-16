using System.Linq;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualisationTests
{
    [TestFixture]
    internal class ListExtentionsTests
    {
        [TestCase(new int[0], 1, ExpectedResult = 0, TestName = "Empty list")]
        [TestCase(new [] {1, 2, 3, 4}, 2, ExpectedResult = 1, TestName = "Simple binary search")]
        [TestCase(new [] {1, 2, 3}, 4, ExpectedResult = 3, TestName = "Not existing element")]
        [TestCase(new [] {1, 2, 2, 2, 3}, 2, ExpectedResult = 1, TestName = "Find first element of repeating")]
        public int BinarySearchTests(int[] elements, int value)
        {
            var list = elements.ToList();

            return list.FindFirstBiggerOrEqualIndex(value);
        }
    }
}
