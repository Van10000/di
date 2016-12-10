using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Painter.BrushSelectors;
using TagsCloudVisualization.Painter.WordsPlacers;
using Point = TagsCloudVisualization.Layouter.Point;
using Rectangle = TagsCloudVisualization.Layouter.Rectangle;
using Size = TagsCloudVisualization.Layouter.Size;

namespace TagsCloudVisualization.Painter
{
    public class PngTextDrawer : ITextDrawer
    {
        private readonly Size imageSize;
        private readonly Color backgroundColor;
        private readonly FontFamily textFontFamily;
        private readonly IWordsPlacer wordsPlacer;
        private readonly IBrushSelector brushSelector;

        public PngTextDrawer(IWordsPlacer wordsPlacer, IBrushSelector brushSelector, FontFamily fontFamily, Color backgroundColor, Size imageSize)
        {
            this.wordsPlacer = wordsPlacer;
            this.brushSelector = brushSelector;
            this.backgroundColor = backgroundColor;
            textFontFamily = fontFamily;
            this.imageSize = imageSize;
        }
        
        public void WritePictureToFile(Dictionary<string, int> wordsStatistics, string filename)
        {
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            var sizes = GetWordsRelativeSizes(wordsStatistics.Keys, graphics);
            var wordsFormatted = wordsPlacer
                .GetWordsFormatted(wordsStatistics, sizes)
                .ToList();
            if (wordsFormatted.Count != 0)
            {
                foreach (var word in wordsFormatted)
                {
                    var brush = brushSelector.SelectBrush(word.Word, wordsStatistics[word.Word]);
                    DrawFormattedWord(graphics, word, brush);
                }
            }
            bitmap.Save(filename);
        }

        private Dictionary<string, SizeF> GetWordsRelativeSizes(IEnumerable<string> words, Graphics g)
        {
            var exampleFont = new Font(textFontFamily, 10);
            return words.ToDictionary(word => word, word => g.MeasureString(word, exampleFont));
        }

        private void DrawFormattedWord(Graphics graphics, WordPlaced word, Brush brush)
        {
            var font = GetFontOfRightSize(word.Word, word.Rectangle, graphics, textFontFamily);
            graphics.DrawString(word.Word, font, brush, word.Rectangle.ToDrawingRectangle());
        }

        private Font GetFontOfRightSize(string str, Rectangle rect, Graphics g, FontFamily fontFamily)
        {
            // binary search for a good font
            double minFontSize = 0;
            double maxFontSize = 100;
            while (maxFontSize - minFontSize > 0.01)
            {
                double fontSize = (minFontSize + maxFontSize) / 2;
                var font = new Font(fontFamily, (float) fontSize);
                if (g.MeasureString(str, font).IsInside(rect.Size))
                    minFontSize = fontSize;
                else
                    maxFontSize = fontSize;
            }
            return new Font(fontFamily, (float) minFontSize);
        }
    }
}
