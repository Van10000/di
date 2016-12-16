using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Layouter;

namespace CircularCloudLayoutTests
{
    [TestFixture]
    public class GeometryTests
    {
        [Test]
        public void NewPoint_CorrectFieldsTest()
        {
            var point = new Point(3, 5);
            point.X.Should().Be(3);
            point.Y.Should().Be(5);
        }

        [Test]
        public void NewSize_CorrectFieldsTest()
        {
            var size = new Size(10, 15);
            size.Width.Should().Be(10);
            size.Height.Should().Be(15);
        }

        [Test]
        public void NegativeSize_IsImpossibleTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Size(-1, 0));
        }

        [Test]
        public void ZeroSize_IsImpossibleTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Size(0, 0));
        }

        [TestCase(1, 1, 1)]
        [TestCase(2, 4, 8)]
        [TestCase(4, 1, 4)]
        [TestCase(3, 7, 21)]
        public void SizeAreaTest(int width, int height, int expectedArea)
        {
            var size = new Size(width, height);

            size.Area.Should().Be(expectedArea);
        }

        [TestCase(1, 0, 1)]
        [TestCase(0, 5, 25)]
        [TestCase(0, 0, 0)]
        [TestCase(3, 4, 25)]
        [TestCase(1, 1, 2)]
        [TestCase(2, 3, 13)]
        [TestCase(-1, -4, 17)]
        public void PointDistanceToZeroTest(int x, int y, double expectedDistanceSquare)
        {
            var zeroPoint = new Point(0, 0);
            var givenPoint = new Point(x, y);

            var distance = zeroPoint.Distance(givenPoint);

            Assert.AreEqual(distance, Math.Sqrt(expectedDistanceSquare), 1e-5);
        }

        [Test]
        public void PointDistanceTest()
        {
            var firts = new Point(2, -4);
            var second = new Point(-3, -7);
            var expectedDistance = 25 + 9;

            var distance = firts.Distance(second);

            Assert.AreEqual(distance, Math.Sqrt(expectedDistance), 1e-5);
        }

        [TestCase(1, 1, 2, 1, 1)]
        [TestCase(2, 2, 2, 1, 1)]
        [TestCase(3, 3, 2, 1, 1)]
        [TestCase(50, 70, 100, 1, 1)]
        [TestCase(4, 6, 2, 2, 3)]
        [TestCase(5, 8, 3, 1, 2)]
        [TestCase(6, 9, 3, 2, 3)]
        public void SizeDivisionTest(int width, int height, int divisor, int expectedWidth, int expectedHeight)
        {
            var size = new Size(width, height);
            var expectedSize = new Size(expectedWidth, expectedHeight);

            var dividedSize = size / divisor;

            dividedSize.Should().Be(expectedSize);
        }

        [Test]
        public void PointMinusPointTest()
        {
            var first = new Point(-1, 9);
            var second = new Point(3, 1);
            var expected = new Point(-4, 8);

            var result = first - second;

            result.Should().Be(expected);
        }

        [Test]
        public void PointMinusSizeTest()
        {
            var point = new Point(-4, 3);
            var size = new Size(2, 5);
            var expected = new Point(-6, -2);

            var result = point - size;

            result.Should().Be(expected);
        }
    }
}
