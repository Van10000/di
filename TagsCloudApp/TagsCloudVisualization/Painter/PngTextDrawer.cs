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
            var wordsFormatted = wordsPlacer.GetWordsFormatted(wordsStatistics);
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            foreach (var word in wordsFormatted)
            {
                var brush = brushSelector.SelectBrush(word.Word, wordsStatistics[word.Word]);
                DrawFormattedWord(graphics, word, brush);
            }
            bitmap.Save(filename);
        }

        private void DrawFormattedWord(Graphics graphics, WordPlaced word, Brush brush)
        {
            Font fontOfRightSize = GetFontOfRightSize(word.Rectangle);
            graphics.DrawString(word.Word, fontOfRightSize, brush, word.Rectangle.ToDrawingRectangle());
        }

        private Font GetFontOfRightSize(Rectangle rect)
        {
            // there is a method to measure string size
            // maybe better way would be to select word rectangle using this method
            return new Font(textFontFamily, 10); //TODO
        }
    }
}
