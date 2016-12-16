using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouter;

namespace CircularCloudLayoutTests
{
    [TestFixture]
    internal class CircularCloudLayouterTests
    {
        private CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            layouter = new CircularCloudLayouter(new Point(0, 0));
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.FailCount != 0)
            {
                var image = RectanglesPainter.GetPicture(layouter.PreviousRectangles);
                var dirPath = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = TestContext.CurrentContext.Test.Name + ".png";
                var fullPath = Path.Combine(dirPath, fileName);
                image.Save(fullPath);
                Console.WriteLine("Tag cloud visualization saved to file <{0}>", fullPath);
            }
        }
        
        [Test]
        public void SingleRectangle_HasRequiredSizeTest()
        {
            var size = new Size(1, 2);

            var rect = layouter.PutNextRectangle(size);

            rect.Size.Should().Be(size);
        }
        
        [Test]
        public void TwoDifferentRectangles_DoNotIntersectTest()
        {
            var sizes = new [] {new Size(5, 2), new Size(3, 7)};

            var rectangles = AddRectangles(sizes);

            AssertRectanglesDoNotIntersect(rectangles);
        }
        
        [TestCase(2, 1, 1)]
        [TestCase(2, 4, 2)]
        [TestCase(3, 1, 1)]
        [TestCase(5, 1, 2)]
        [TestCase(100, 5, 7)]
        public void ManyEqualRectangles_DoNotIntersectTest(int rectanglesNumber, int width, int height)
        {
            var sizes = RepeatSizes(rectanglesNumber, width, height);

            var rectangles = AddRectangles(sizes);

            AssertRectanglesDoNotIntersect(rectangles);
        }

        [TestCase(10, 1, 2)]
        [TestCase(10, 1, 10)]
        [TestCase(100, 13, 3)]
        [TestCase(300, 40, 60)]
        public void ManyRectangles_ShapeCircleTest(int rectanglesNumber, int width, int height)
        {
            var sizes = RepeatSizes(rectanglesNumber, width, height);

            var rectangles = AddRectangles(sizes);

            AssertIsApproximatelyCircle(rectangles, 2);
        }

        [TestCase(20, 300, 300, TestName = "Big")]
        [TestCase(30, 5, 20, TestName = "Tall")]
        [TestCase(50, 50, 50, TestName = "Random")]
        [TestCase(200, 80, 10, TestName = "Long")]
        public void ManyRandomRectangles_ShapeCircleTest(int rectanglesNumber, int maxWidth, int maxHeight)
        {
            var rand = new Random(0);
            var sizes =
                Enumerable.Repeat(new Size(1, 1), rectanglesNumber)
                .Select(_ => GetRandomSize(rand, maxWidth, maxHeight))
                .ToArray();

            var rectangles = AddRectangles(sizes);

            AssertIsApproximatelyCircle(rectangles, 2);
        }

        [Test]
        public void SingleRectangle_ShapeCircleTest()
        {
            var sizes = RepeatSizes(1, 1, 100);

            var rectangles = AddRectangles(sizes);

            AssertIsApproximatelyCircle(rectangles, 0.5);
        }
        
        private void AssertIsApproximatelyCircle(Rectangle[] rectangles, double strictnessCoefficient)
        {
            var sumArea = rectangles.Sum(rect => rect.Size.Area);
            var minimalCircleRadius = Math.Sqrt(sumArea / Math.PI);
            var margin = rectangles.Max(rect => Math.Max(rect.Size.Width, rect.Size.Height)) * strictnessCoefficient;

            rectangles
                .SelectMany(rect => rect.GetPoints())
                .All(point => point.Distance(new Point(0, 0)) < minimalCircleRadius + margin)
                .Should().BeTrue();
        }

        private Size GetRandomSize(Random rand, int maxWidth, int maxHeight)
        {
            return new Size(rand.Next() % maxWidth + 1, rand.Next() % maxHeight + 1);
        }
        
        private Size[] RepeatSizes(int count, int width, int height)
        {
            return Enumerable.Repeat(new Size(width, height), count).ToArray();
        }

        private Rectangle[] AddRectangles(params Size[] sizes)
        {
            return sizes.Select(t => layouter.PutNextRectangle(t)).ToArray();
        }

        private void AssertRectanglesDoNotIntersect(params Rectangle[] rectangles)
        {
            for (int i = 0; i < rectangles.Length; ++i)
                for (int j = i + 1; j < rectangles.Length; ++j)
                    rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
        }
    }
}
