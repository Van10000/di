using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
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
using Point = TagsCloudVisualization.Layouter.Point;
using Size = TagsCloudVisualization.Layouter.Size;

namespace ConsoleTagsCloudApp
{
    class Program
    {
        private static readonly Dictionary<BrushSelectorType, Action<Options, WindsorContainer, Dictionary<string, int>>> brushSelectorRegisterActions = 
            new Dictionary<BrushSelectorType, Action<Options, WindsorContainer, Dictionary<string, int>>>()
        {
            {BrushSelectorType.Random, (options, container, stat) => RegisterRandomBrushSelector(container)},
            {BrushSelectorType.Single, (options, container, stat) => RegisterSingleColorBrushSelector(container, options, stat)},
            {BrushSelectorType.Gradient, (options, container, stat) => RegisterGradientColorBrushSelector(container, options, stat)}
        };

        static void Main(string[] args)
        {
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine("Arguments given are incorrect");
                return;
            }

            var container = new WindsorContainer();

            RegisterWordsSelectorComponents(container, options);
            var statisticsSupplier = container.Resolve<WordsStatisticsSupplier>();
            var statisticsSelector = container.Resolve<ComplexWordsSelector>();
            var wordsStatistics = statisticsSelector.SelectWords(statisticsSupplier.GetWordsStatistics());

            RegisterTextDrawerComponents(container, options, wordsStatistics);
            var textDrawer = container.Resolve<ITextDrawer>();
            textDrawer.WritePictureToFile(wordsStatistics, options.ImageOutputFile);
        }

        private static Color ToColor(string[] channels)
        {
            if (channels.Length != 4)
                throw new ArgumentException("There should be exactly 4 channels of color: red, green, blue and alpha.");
            var intChannels = channels.Select(int.Parse).ToArray();
            return Color.FromArgb(intChannels[0], intChannels[1], intChannels[2], intChannels[3]);
        }

        private static void RegisterRandomBrushSelector(WindsorContainer container)
        {
            container.Register(
                Component
                    .For<IBrushSelector>()
                    .ImplementedBy<RandomColorBrushSelector>());
        }

        private static void RegisterSingleColorBrushSelector(WindsorContainer container, Options options, Dictionary<string, int> wordsStatistics)
        {
            container.Register(
                Component
                    .For<IBrushSelector>()
                    .ImplementedBy<SingleColorBrushSelector>()
                    .DependsOn(Dependency.OnValue("wordsStatistics", wordsStatistics))
                    .DependsOn(Dependency.OnValue("color", ToColor(options.TextColor))));
        }

        private static void RegisterGradientColorBrushSelector(WindsorContainer container, Options options, Dictionary<string, int> wordsStatistics)
        {
            container.Register(
                Component
                    .For<IBrushSelector>()
                    .ImplementedBy<GradientBrushSelector>()
                    .DependsOn(Dependency.OnValue("wordsStatistics", wordsStatistics))
                    .DependsOn(Dependency.OnValue("mostFrequentWordColor", ToColor(options.MostFrequentTextColor)))
                    .DependsOn(Dependency.OnValue("leastFrequentWordColor", ToColor(options.LeastFrequentTextColor))));
        }

        private static void RegisterTextDrawerComponents(WindsorContainer container, Options options, Dictionary<string, int> wordsStatistics)
        {
            var imageSize = new Size(options.ImageWidth, options.ImageHeight);
            var backgroundColor = ToColor(options.BackgroundColor);
            var fontFamily = new FontFamily(options.FontFamily);

            container.Register(
                Component
                    .For<ILayouter>()
                    .ImplementedBy<CircularCloudLayouter>());

            container.Register(
                Component.For<FontFamily>().Instance(fontFamily));
            container.Register(
                Component.For<Size>().Instance(imageSize));
            container.Register(
                Component.For<IWordsPlacer>().ImplementedBy<LinearAreaGrowthWordsPlacer>());
            brushSelectorRegisterActions[options.TextBrushSelector](options, container, wordsStatistics);
            container.Register(
                Component
                    .For<ITextDrawer>()
                    .ImplementedBy<PngTextDrawer>()
                    .DependsOn(Dependency.OnValue("backgroundColor", backgroundColor)));
        }

        private static void RegisterWordsSelectorComponents(WindsorContainer container, Options options)
        {
            container.Register(
                Component
                    .For<ITextSupplier>()
                    .ImplementedBy<TxtFileTextSupplier>()
                    .DependsOn(Dependency.OnValue("filename", options.TextInputFile)));
            container.Register(
                Component.For<WordsStatisticsSupplier>()
                );
            container.Register(
                Component
                    .For<IWordsSelector>()
                    .ImplementedBy<LowerCaseWordsSelector>(),

                Component
                    .For<IWordsSelector>()
                    .ImplementedBy<ExcludeLowLengthWordsSelector>(),

                Component
                    .For<IWordsSelector>()
                    .ImplementedBy<ExcludeRareWordsSelector>()
                    .DependsOn(Dependency.OnValue("maximalAllowedWordsNumber", options.MaximalAllowedWordsNumber)));
            container.Register(
                Component
                    .For<ComplexWordsSelector>()
                    .DependsOn(Dependency.OnValue("wordsSelectors", container.ResolveAll<IWordsSelector>())));
        }
    }
}
