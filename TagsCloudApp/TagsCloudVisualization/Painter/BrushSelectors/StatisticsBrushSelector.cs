using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Painter.BrushSelectors
{
    public abstract class StatisticsBrushSelector
    {
        protected Dictionary<string, int> WordsStatistics;
         
        protected StatisticsBrushSelector(Dictionary<string, int> wordsStatistics)
        {
            WordsStatistics = wordsStatistics;
        }
    }
}
