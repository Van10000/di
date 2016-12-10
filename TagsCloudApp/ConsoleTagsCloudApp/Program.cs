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
        static void Main(string[] args)
        {
            var options = new Options();
            options.TextInputFile = "text.txt";
            options.ImageOutputFile = "image.png";
            options.ImageWidth = 5000;
            options.ImageHeight = 5000;
            //if (!CommandLine.Parser.Default.ParseArguments(args, options))
            //{
            //    Console.WriteLine("Arguments given are incorrect");
            //    return;
            //}
            var imageSize = new Size(options.ImageWidth, options.ImageHeight);
            var backgroundColor = Color.Black;
            var fontFamily = new FontFamily("Times New Roman");
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());
            container.Register(
                Component
                .For<ITextSupplier>()
                .ImplementedBy<TxtFileTextSupplier>()
                .DependsOn(Dependency.OnValue("filename", options.TextInputFile))
                );
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
                .DependsOn(Dependency.OnValue("maximalAllowedWordsNumber", 300))
                );
            container.Register(
                Component
                .For<ComplexWordsSelector>()
                .DependsOn(Dependency.OnValue("wordsSelectors", container.ResolveAll<IWordsSelector>()))
                );

            container.Register(
                Component
                .For<ILayouter>()
                .ImplementedBy<CircularCloudLayouter>()
                .DependsOn(Dependency.OnValue("center", new Point(0, 0)))
                );
            
            container.Register(
                Component.For<FontFamily>().Instance(fontFamily));
            container.Register(
                Component.For<Size>().Instance(imageSize)
                );
            container.Register(
                Component.For<IWordsPlacer>().ImplementedBy<LinearAreaGrowthWordsPlacer>()
                );
            container.Register(
                Component.For<IBrushSelector>().ImplementedBy<RandomColorBrushSelector>()
                );
            container.Register(
                Component
                .For<ITextDrawer>()
                .ImplementedBy<PngTextDrawer>()
                .DependsOn(Dependency.OnValue("backgroundColor", backgroundColor))
                );
            var statisticsSupplier = container.Resolve<WordsStatisticsSupplier>();
            var statisticsSelector = container.Resolve<ComplexWordsSelector>();
            var wordsStatistics = statisticsSelector.SelectWords(statisticsSupplier.GetWordsStatistics());
            var textDrawer = container.Resolve<ITextDrawer>();
            textDrawer.WritePictureToFile(wordsStatistics, options.ImageOutputFile);
        }
    }
}
