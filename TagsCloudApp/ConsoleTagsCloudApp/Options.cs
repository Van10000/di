using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using TagsCloudVisualization.Layouter;

namespace ConsoleTagsCloudApp
{
    public class Options
    {
        [Option('t', "text_input_file", HelpText = "Name of file with text containing words to build tags cloud.", Required = true)]
        public string TextInputFile { get; set; }

        [Option('i', "image_output_file", HelpText = "Name of file to output image of tags cloud", Required = true)]
        public string ImageOutputFile { get; set; }
        
        //[Option('a', "filtering_algo", DefaultValue = "exclude_low_length", HelpText = "Name of algo to filter words from the text")]
        //public string WordsFilteringAlgorithm { get; set; } // TODO: use enums here

        [OptionArray('b', "background_color", DefaultValue = new [] {"255", "0", "0", "0"}, 
            HelpText = "alpha, reg, green and blue channels of background color")]
        public string[] BackgroundColor { get; set; }

        [Option('s', "text_brush_selector", DefaultValue = BrushSelectorType.Random, HelpText = "Brush selector type you want to be used for text")]
        public BrushSelectorType TextBrushSelector { get; set; }

        [OptionArray('c', "text_color", DefaultValue = null,
            HelpText = "Alpha, red, green and blue channel of text color. Need only for single brush selectors.")]
        public string[] TextColor { get; set; }

        [OptionArray('m', "most_frequent_color", DefaultValue = null,
            HelpText = "Alpha, red, green and blue channel of most frequent words color. Need only for gradient brush selectors.")]
        public string[] MostFrequentTextColor { get; set; }

        [OptionArray('l', "least_frequent_color", DefaultValue = null,
            HelpText = "Alpha, red, green and blue channel of least frequent words color. Need only for gradient brush selectors.")]
        public string[] LeastFrequentTextColor { get; set; }

        [Option('f', "font", DefaultValue = "Times New Roman", HelpText = "Font of text in the image")]
        public string FontFamily { get; set; }

        [Option('w', "image_width", DefaultValue = 500, HelpText = "Width of resulting image with tags cloud")]
        public int ImageWidth { get; set; }

        [Option('h', "image_height", DefaultValue = 500, HelpText = "Height of resulting image with tags cloud")]
        public int ImageHeight { get; set; }

        [Option('n', "maximal_allowed_words_number", DefaultValue = 300, 
            HelpText = "Number of words to draw in tags cloud. If there aren't enough words - draw all of them.")]
        public int MaximalAllowedWordsNumber { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
