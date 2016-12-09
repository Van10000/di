using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Castle.Windsor;
using Castle.Windsor.Installer;
using TagsCloudApplication.TextSuppliers;
using TagsCloudApplication.WordsSelectors;
using TagsCloudApplication.WordsStatisticsSuppliers;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Painter;
using TagsCloudVisualization.Painter.BrushSelectors;
using TagsCloudVisualization.Painter.WordsPlacers;

namespace ConsoleTagsCloudApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            options.ImageOutputFile = "image.png";
            //if (!CommandLine.Parser.Default.ParseArguments(args, options))
            //{
            //    Console.WriteLine("Arguments given are incorrect");
            //    return;
            //}
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());
            //var text = new TxtFileTextSupplier(options.TextInputFile).SupplyText();
            var text = "Some text, text, and text, actually some is also two times, and times.";
            var wordsStatistics = new WordsStatisticsSupplier(text).GetWordsStatistics();
            var wordsSelector = new ComplexWordsSelector(
                new LowerCaseWordsSelector(),
                new ExcludeLowLengthWordsSelector());
            wordsStatistics = wordsSelector.SelectWords(wordsStatistics);
            var brushSelector = new RandomColorBrushSelector();
            var wordsPlacer = new LinearAreaGrowthWordsPlacer();
            var imageSize = new Size(wordsPlacer.ImageWidth, wordsPlacer.ImageHeight);
            var fontFamily = new System.Drawing.FontFamily("Times New Roman");
            var backgroundColor = System.Drawing.Color.Black;
            var textDrawer = new PngTextDrawer(wordsPlacer, brushSelector, fontFamily, backgroundColor, imageSize);
            textDrawer.WritePictureToFile(wordsStatistics, options.ImageOutputFile);
        }
    }
}
