using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTagsCloudApp;
using FluentAssertions;
using NUnit.Framework;

namespace ConsoleTagsCloudAppTests
{
    [TestFixture]
    public class ColorParserTests
    {
        [TestCase(new[] { "94", "184", "76", "77" }, new [] { 94, 184, 76, 77 })]
        [TestCase(new[] { "76", "244", "16" }, new [] { 255, 76, 244, 16 })]
        [TestCase(new[] { "#10101010"}, new[] { 16, 16, 16, 16 })]
        [TestCase(new[] { "#050702"}, new[] { 255, 5, 7, 2 })]
        [TestCase(new[] { "White"}, new[] { 255, 255, 255, 255 })]
        public void ParseTest(string[] channels, int[] expectedColorChannels)
        {
            var expectedColor = ToColor(expectedColorChannels);

            var parsed = ColorParser.Parse(channels);

            AssertColorsAreEqual(parsed, expectedColor);
        }

        private Color ToColor(int[] channels)
        {
            return Color.FromArgb(channels[0], channels[1], channels[2], channels[3]);
        }

        private void AssertColorsAreEqual(Color first, Color second)
        {
            first.A.Should().Be(second.A);
            first.R.Should().Be(second.R);
            first.G.Should().Be(second.G);
            first.B.Should().Be(second.B);
        }
    }
}
