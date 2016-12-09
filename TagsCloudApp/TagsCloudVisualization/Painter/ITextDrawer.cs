using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Painter
{
    public interface ITextDrawer
    {
        void WritePictureToFile(Dictionary<string, int> wordsStatistics, string filename);
    }
}
