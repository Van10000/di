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
        
        [Option('a', "filtering_algo", DefaultValue = "exclude_low_length", HelpText = "Name of algo to filter words from the text")]
        public string WordsFilteringAlgorithm { get; set; }

        [Option('w', "image_width", DefaultValue = 500, HelpText = "Width of resulting image with tags cloud")]
        public int ImageWidth { get; set; }

        [Option('h', "image_height", DefaultValue = 500, HelpText = "Height of resulting image with tags cloud")]
        public int ImageHeight { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
