using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagsCloudApplication.TextSuppliers;
using TagsCloudApplication.WordsSelectors;
using TagsCloudApplication.WordsStatisticsSuppliers;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Painter;
using TagsCloudVisualization.Painter.BrushSelectors;
using TagsCloudVisualization.Painter.WordsPlacers;
using Size = TagsCloudVisualization.Layouter.Size;

namespace ConsoleTagsCloudApp
{
    internal static class WindsorContainerUtils
    {
        private static readonly Dictionary<BrushSelectorType, Action<Options, WindsorContainer, Dictionary<string, int>>> BrushSelectorRegisterActions =
            new Dictionary<BrushSelectorType, Action<Options, WindsorContainer, Dictionary<string, int>>>()
        {
            {BrushSelectorType.Random, (options, container, stat) => RegisterRandomBrushSelector(container)},
            {BrushSelectorType.Single, (options, container, stat) => RegisterSingleColorBrushSelector(container, options, stat)},
            {BrushSelectorType.Gradient, (options, container, stat) => RegisterGradientColorBrushSelector(container, options, stat)}
        };

        private static readonly Dictionary<FilteringAlgorithmName, Action<WindsorContainer, Options>> WordsSelectorRegisterActions = 
            new Dictionary<FilteringAlgorithmName, Action<WindsorContainer, Options>>()
        {
            {FilteringAlgorithmName.ExcludeBoring, RegisterExcludeBoringWordsSelector },
            {FilteringAlgorithmName.ExcludeLowLength, RegisterExcludeLowLengthSelector }
        };
        
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
                    .DependsOn(Dependency.OnValue<Color>(ColorParser.Parse(options.TextColor))));
        }

        private static void RegisterGradientColorBrushSelector(WindsorContainer container, Options options, Dictionary<string, int> wordsStatistics)
        {
            var colorPair = new GradientColorsPair(
                ColorParser.Parse(options.MostFrequentTextColor),
                ColorParser.Parse(options.LeastFrequentTextColor));

            container.Register(
                Component
                    .For<IBrushSelector>()
                    .ImplementedBy<GradientBrushSelector>()
                    .DependsOn(Dependency.OnValue<IEnumerable<int>>(wordsStatistics.Select(pair => pair.Value)))
                    .DependsOn(Dependency.OnValue<GradientColorsPair>(colorPair)));
        }

        private static void RegisterExcludeLowLengthSelector(WindsorContainer container, Options options)
        {
            container.Register(
                Component
                    .For<IWordsSelector>()
                    .ImplementedBy<ExcludeLowLengthWordsSelector>()
                    .DependsOn(Dependency.OnValue<int>(4)));
        }

        private static void RegisterExcludeBoringWordsSelector(WindsorContainer container, Options options)
        {
            container.Register(
                Component
                    .For<IWordsSelector>()
                    .ImplementedBy<ExcludeLowLengthWordsSelector>()
                    .DependsOn(Dependency.OnValue<int>(2)),
                Component
                    .For<IWordsSelector>()
                    .ImplementedBy<ExcludeBoringWordsSelector>());
        }

        public static void RegisterTextDrawerComponents(WindsorContainer container, Options options, Dictionary<string, int> wordsStatistics)
        {
            var imageSize = new Size(options.ImageWidth, options.ImageHeight);
            var backgroundColor = ColorParser.Parse(options.BackgroundColor);
            var fontFamily = new FontFamily(options.FontFamily);

            container.Register(
                Component
                    .For<ILayouter>()
                    .ImplementedBy<CircularCloudLayouter>());
            container.Register(
                Component
                    .For<IWordsPlacer>()
                    .ImplementedBy<LinearAreaGrowthWordsPlacer>()
                    .DependsOn(Dependency.OnValue<Size>(imageSize)));
            BrushSelectorRegisterActions[options.TextBrushSelector](options, container, wordsStatistics);
            container.Register(
                Component
                    .For<ITextDrawer>()
                    .ImplementedBy<PngTextDrawer>()
                    .DependsOn(Dependency.OnValue<Color>(backgroundColor))
                    .DependsOn(Dependency.OnValue<Size>(imageSize))
                    .DependsOn(Dependency.OnValue<FontFamily>(fontFamily)));
        }

        public static void RegisterWordsSelectorComponents(WindsorContainer container, Options options)
        {
            container.Register(
                Component
                    .For<ITextSupplier>()
                    .ImplementedBy<TxtFileTextSupplier>()
                    .DependsOn(Dependency.OnValue<string>(options.TextInputFile)));
            container.Register(
                Component.For<WordsStatisticsSupplier>());
            container.Register(
                Component
                    .For<IWordsSelector>()
                    .ImplementedBy<LowerCaseWordsSelector>());
            WordsSelectorRegisterActions[options.WordsFilteringAlgorithm](container, options);
            container.Register(
                Component
                    .For<IWordsSelector>()
                    .ImplementedBy<ExcludeRareWordsSelector>()
                    .DependsOn(Dependency.OnValue<int>(options.MaximalAllowedWordsNumber)));

            container.Register(
                Component
                    .For<ComplexWordsSelector>()
                    .DependsOn(Dependency.OnValue<IWordsSelector[]>(container.ResolveAll<IWordsSelector>())));
        }
    }
}
