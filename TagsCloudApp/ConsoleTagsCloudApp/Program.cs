using System;
using CommandLine;
using Castle.Windsor;
using TagsCloudApplication.WordsSelectors;
using TagsCloudApplication.WordsStatisticsSuppliers;
using TagsCloudVisualization.Painter;
using Utils;

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
                    "-n", "100",
                    "-b", "255", "30", "30", "30"
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

            var result =
                from statSupplier in Resolve<WordsStatisticsSupplier>(container)
                from statSelector in Resolve<ComplexWordsSelector>(container)
                from wordsStat in statSupplier.GetWordsStatistics().Then(statSelector.SelectWords)
                from register in WindsorContainerUtils.RegisterTextDrawerComponents(container, options, wordsStat)
                from textDrawer in Resolve<ITextDrawer>(container)
                from drawingResult in textDrawer.WritePictureToFile(wordsStat, options.ImageOutputFile)
                select drawingResult;

            if (!result.IsSuccess)
                Console.WriteLine(result.Error);
        }

        private static Result<T> Resolve<T>(WindsorContainer container)
        {
            return Result
                .Of(container.Resolve<T>)
                .ReplaceError(str => "Could not load components of program.");
        }
    }
}
