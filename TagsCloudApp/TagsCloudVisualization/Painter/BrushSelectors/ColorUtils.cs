using System;
using System.Drawing;

namespace TagsCloudVisualization.Painter.BrushSelectors
{
    internal static class ColorUtils
    {
        public static Color GetInRatio(Color first, Color second, double ratio)
        {
            var A = GetInRatio(first.A, second.A, ratio);
            var R = GetInRatio(first.R, second.R, ratio);
            var G = GetInRatio(first.G, second.G, ratio);
            var B = GetInRatio(first.B, second.B, ratio);
            return Color.FromArgb(A, R, G, B);
        }

        public static Color GetRandomColor(Random rand)
            => Color.FromArgb(127, rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));

        private static int GetInRatio(int first, int second, double ratio)
            => (int)(first * ratio + second * (1 - ratio));
    }
}
