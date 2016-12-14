using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using java.util;
using WiktionaryNET;

namespace TagsCloudApplication.WordsSelectors
{
    public class ExcludePartsOfSpeechSelector : PredicateWordsSelector
    {
        public const string Verb = "Verb";
        public const string Noun = "Noun";
        public const string Adjective = "Adjective";
        public const string Adverb = "Adverb";
        public const string Pronoun = "Pronoun";
        public const string Preposition = "Preposition";
        public const string Conjunction = "Conjunction";
        public const string Interjection = "Interjection";

        //private static readonly string taggerPath =
        //    @"C:\Users\ISmir\Desktop\учёба\2 курс\шпора\dependency injection\di\TagsCloudApp\TagsCloudApplication";

        //private static readonly MaxentTagger Tagger = new MaxentTagger(Path.Combine(taggerPath, @"english-caseless-left3words-distsim.tagger"));

        public ExcludePartsOfSpeechSelector(params string[] partsOfSpeech) : base(word => ShouldExclude(partsOfSpeech, word))
        {
        }

        public static bool ShouldExclude(string[] excludingPartsOfSpeech, string word)
        {
            //var wordInfo = Tagger.tagString(word);
            //return excludingPartsOfSpeech.Any(part => wordInfo.Contains(part));
            return true;
        }
    }
}
