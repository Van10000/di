using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.Painter.BrushSelectors;

namespace TagsCloudVisualisationTests
{
    public class ColorUtilsTests
    {
        [TestCase(0)]
        [TestCase(0.5)]
        [TestCase(0.67657165238126)]
        [TestCase(1)]
        public void CorrectSingleColor(double ratio)
        {
            var color = Color.FromArgb(124, 96, 45, 1);

            var changed = ColorUtils.GetInRatio(color, color, ratio);

            changed.Should().Be(color);
        }

        [Test]
        public void CorrectColorFromRatio()
        {
            var first = Color.FromArgb(20, 20, 20, 0);
            var second = Color.FromArgb(80, 50, 26, 60);
            var ratio = 1.0 / 3;

            var changed = ColorUtils.GetInRatio(first, second, ratio);

            changed.Should().Be(Color.FromArgb(60, 40, 24, 40));
        }
    }
}
