using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Layouter;

namespace CircularCloudLayoutTests
{
    [TestFixture]
    internal class RectangleTests
    {
        [Test]
        public void NewRectangle_CorrectFieldsTest()
        {
            var rect = new Rectangle(new Point(2, 3), 5, 1);
            rect.LeftDown.Should().Be(new Point(2, 3));
            rect.Size.Should().Be(new Size(5, 1));
        }

        [Test]
        public void Rectangles_IntersectTest()
        {
            var first = new Rectangle(new Point(0, 0), 2, 2);
            var second = new Rectangle(new Point(1, 1), 2, 2);
            first.IntersectsWith(second).Should().BeTrue();
        }

        [Test]
        public void RectanglesSameY_DoNotIntersectTest()
        {
            var first = new Rectangle(new Point(0, 0), 1, 2);
            var second = new Rectangle(new Point(2, 0), 1, 1);
            first.IntersectsWith(second).Should().BeFalse();
        }

        [Test]
        public void RectanglesSameX_DoNotIntersectTest()
        {
            var first = new Rectangle(new Point(0, 0), 1, 3);
            var second = new Rectangle(new Point(0, 4), 1, 3);
            first.IntersectsWith(second).Should().BeFalse();
        }

        [Test]
        public void RectanglesWithCommonLine_DoNotIntersectTest()
        {
            var first = new Rectangle(new Point(0, 0), 1, 4);
            var second = new Rectangle(new Point(0, 4), 1, 4);
            first.IntersectsWith(second).Should().BeFalse();
        }

        [Test]
        public void RectanglesWithMinimalOverlap_IntersectTest()
        {
            var first = new Rectangle(new Point(0, 0), 1, 5);
            var second = new Rectangle(new Point(0, 4), 1, 1);
            first.IntersectsWith(second).Should().BeTrue();
        }

        [Test]
        public void RectanglesOneInsideAnother_IntersectTest()
        {
            var first = new Rectangle(new Point(0, 0), 4, 4);
            var second = new Rectangle(new Point(1, 1), 2, 2);
            first.IntersectsWith(second).Should().BeTrue();
        }

        [Test]
        public void GetPoints_CorrectResultTest()
        {
            var rect = new Rectangle(new Point(0, 0), 2, 4);
            rect.GetPoints()
                .ShouldAllBeEquivalentTo(new[] {new Point(0, 0), new Point(2, 0), new Point(0, 4), new Point(2, 4)});
        }
    }
}
