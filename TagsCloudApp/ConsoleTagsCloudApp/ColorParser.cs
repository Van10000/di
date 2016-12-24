using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace ConsoleTagsCloudApp
{
    internal static class ColorParser
    {
        public static Color HexToColor(string hex)
        {
            return ColorTranslator.FromHtml(hex);
        }

        public static Color ChannelsToColor(int[] channels)
        {
            if (channels.Length == 4)
                return Color.FromArgb(channels[0], channels[1], channels[2], channels[3]);
            return Color.FromArgb(channels[0], channels[1], channels[2]);
        }

        public static Result<Color> Parse(string[] stringRepresentation)
        {
            if (stringRepresentation == null)
                return Result.Fail<Color>("Color not specified");
            if (stringRepresentation.Length == 1)
                return Result.Of(() => HexToColor(stringRepresentation[0]));
            if (stringRepresentation.Length == 4 || stringRepresentation.Length == 3)
                return Result.Of(() => ChannelsToColor(stringRepresentation.Select(int.Parse).ToArray()));
            return Result.Fail<Color>("Unknown color representation."); 
        }
    }
}
