using CommandLine;
using CommandLine.Text;

namespace ConsoleTagsCloudApp
{
    public class Options
    {
        public const string ColorHelp =
            "Alpha, red, green and blue channel of most frequent words color.\n" +
            "Or hex representation.\n" +
            "Or color name.";

        [Option('t', "text_input_file", HelpText = "Name of file with text containing words to build tags cloud.", Required = true)]
        public string TextInputFile { get; set; }
        
        [Option('i', "image_output_file", HelpText = "Name of file to output image of tags cloud", Required = true)]
        public string ImageOutputFile { get; set; }

        [Option('a', "filtering_algo", DefaultValue = FilteringAlgorithmName.ExcludeBoring, HelpText = "Name of algo to filter words from the text. Possible names are ExcludeLowLength and ExcludeBoring")]
        public FilteringAlgorithmName WordsFilteringAlgorithm { get; set; }

        [OptionArray('b', "background_color", DefaultValue = new [] {"255", "0", "0", "0"}, 
            HelpText = ("Background color in specified format:\n" + ColorHelp))]
        public string[] BackgroundColor { get; set; }

        [Option('s', "text_brush_selector", DefaultValue = BrushSelectorType.Random, HelpText = "Brush selector type you want to be used for text. Possible types are Random, Single and Gradient")]
        public BrushSelectorType TextBrushSelector { get; set; }

        [OptionArray('c', "text_color", DefaultValue = null,
            HelpText = ColorHelp + "\nNeed only for single brush selectors.")]
        public string[] TextColor { get; set; }

        [OptionArray('m', "most_frequent_color", DefaultValue = null,
            HelpText = ColorHelp + "\nNeed only for gradient brush selectors.")]
        public string[] MostFrequentTextColor { get; set; }

        [OptionArray('l', "least_frequent_color", DefaultValue = null,
            HelpText = ColorHelp + "\nNeed only for gradient brush selectors.")]
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
