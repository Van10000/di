using System;
using CommandLine;
using Castle.Windsor;
using TagsCloudApplication.WordsSelectors;
using TagsCloudApplication.WordsStatisticsSuppliers;
using TagsCloudVisualization.Painter;

namespace ConsoleTagsCloudApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new[]
                {
                    "-t", "text.txt",
                    "-i", "image.png",
                    "-h", "5000",
                    "-w", "5000",
                    "-n", "300",
                    "-b", "255", "30", "30", "30",
                    "-s", "Gradient",
                    "-l", "#96ff96",
                    "-m", "Red"
                };
            }
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine("Arguments given are incorrect");
                return;
            }

            var container = new WindsorContainer();

            WindsorContainerUtils.RegisterWordsSelectorComponents(container, options);
            var statisticsSupplier = container.Resolve<WordsStatisticsSupplier>();
            var statisticsSelector = container.Resolve<ComplexWordsSelector>();
            var wordsStatistics = statisticsSelector.SelectWords(statisticsSupplier.GetWordsStatistics());

            WindsorContainerUtils.RegisterTextDrawerComponents(container, options, wordsStatistics);
            var textDrawer = container.Resolve<ITextDrawer>();
            textDrawer.WritePictureToFile(wordsStatistics, options.ImageOutputFile);
        }
    }
}
