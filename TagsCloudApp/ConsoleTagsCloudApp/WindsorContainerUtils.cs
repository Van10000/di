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
using Utils;
using Size = TagsCloudVisualization.Layouter.Size;

namespace ConsoleTagsCloudApp
{
    internal static class WindsorContainerUtils
    {
        private static readonly Dictionary<BrushSelectorType, Func<Options, WindsorContainer, Dictionary<string, int>, Result<None>>> BrushSelectorRegisterActions =
            new Dictionary<BrushSelectorType, Func<Options, WindsorContainer, Dictionary<string, int>, Result<None>>>()
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
        
        private static Result<None> RegisterRandomBrushSelector(WindsorContainer container)
        {
            container.Register(
                Component
                    .For<IBrushSelector>()
                    .ImplementedBy<RandomColorBrushSelector>());
            return Result.Ok();
        }

        private static Result<None> RegisterSingleColorBrushSelector(WindsorContainer container, Options options, Dictionary<string, int> wordsStatistics)
        {
            return ColorParser.Parse(options.TextColor)
                .Then(color =>
                container.Register(
                    Component
                        .For<IBrushSelector>()
                        .ImplementedBy<SingleColorBrushSelector>()
                        .DependsOn(Dependency.OnValue<Color>(color))))
                .Then(x => { });
        }

        private static Result<None> RegisterGradientColorBrushSelector(WindsorContainer container, Options options, Dictionary<string, int> wordsStatistics)
        {
            var colorsPair = 
                from mostFrequentColor in ColorParser.Parse(options.MostFrequentTextColor).RefineError("Most frequent color incorrect")
                from leastFrequentColor in ColorParser.Parse(options.LeastFrequentTextColor).RefineError("Least frequent color incorrect")
                select new GradientColorsPair(mostFrequentColor, leastFrequentColor);

            return colorsPair
                .Then(colors => container.Register(
                    Component
                        .For<IBrushSelector>()
                        .ImplementedBy<GradientBrushSelector>()
                        .DependsOn(Dependency.OnValue<IEnumerable<int>>(wordsStatistics.Select(pair => pair.Value)))
                        .DependsOn(Dependency.OnValue<GradientColorsPair>(colors))))
                .Then(x => { });
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

        public static Result<None> RegisterTextDrawerComponents(WindsorContainer container, Options options, Dictionary<string, int> wordsStatistics)
        {
            var imageSize = Result.Of(() => new Size(options.ImageWidth, options.ImageHeight)).RefineError("Incorrect size");
            var backgroundColor = ColorParser.Parse(options.BackgroundColor).RefineError("Incorrect background color");
            var fontFamily = Result.Of(() => new FontFamily(options.FontFamily)).RefineError("Incorrect font family");
            
            container.Register(
                Component
                    .For<ILayouter>()
                    .ImplementedBy<CircularCloudLayouter>());
            var placerRegisterResult = imageSize.Then(size =>
            container.Register(
                Component
                    .For<IWordsPlacer>()
                    .ImplementedBy<LinearAreaGrowthWordsPlacer>()
                    .DependsOn(Dependency.OnValue<Size>(size))));

            var brushRegisterResult = BrushSelectorRegisterActions[options.TextBrushSelector](options, container, wordsStatistics);

            var returnResult = from placerRes in placerRegisterResult
                               from brushRes in brushRegisterResult
                               from backColor in backgroundColor
                               from size in imageSize
                               from fontFam in fontFamily
                               select
            container.Register(
                Component
                    .For<ITextDrawer>()
                    .ImplementedBy<PngTextDrawer>()
                    .DependsOn(Dependency.OnValue<Color>(backColor))
                    .DependsOn(Dependency.OnValue<Size>(size))
                    .DependsOn(Dependency.OnValue<FontFamily>(fontFam)));

            return returnResult.Then(x => { });
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
