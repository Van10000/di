using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Painter.BrushSelectors
{
    public abstract class StatisticsBrushSelector : IBrushSelector
    {
        protected Dictionary<string, int> WordsStatistics;
         
        protected StatisticsBrushSelector(Dictionary<string, int> wordsStatistics)
        {
            WordsStatistics = wordsStatistics;
        }

        public abstract Brush SelectBrush(string word, int count);
    }
}
