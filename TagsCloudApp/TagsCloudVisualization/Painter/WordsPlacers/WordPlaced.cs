using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization.Painter.WordsPlacers
{
    public class WordPlaced
    {
        public readonly string Word;
        public readonly Rectangle Rectangle;

        public WordPlaced(string word, Rectangle rectangle)
        {
            Word = word;
            Rectangle = rectangle;
        }
    }
}
