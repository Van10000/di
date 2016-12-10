using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Test]
        public void Check()
        {
            var fontFamily = new FontFamily("Times New Roman");
            var font10 = new Font(fontFamily, 10);
            var font20 = new Font(fontFamily, 20);
            var font30 = new Font(fontFamily, 30);
            var text = "Hello, world!";
            var bitmap = new Bitmap(500, 500);
            var graphics = Graphics.FromImage(bitmap);
            var mes1 = graphics.MeasureString(text, font10);
            var mes2 = graphics.MeasureString(text, font20);
            var mes3 = graphics.MeasureString(text, font30);
            var mes33 = graphics.MeasureString(text + text + text, font10); // width here differs from mes3 width
            return;
        }
    }
}
