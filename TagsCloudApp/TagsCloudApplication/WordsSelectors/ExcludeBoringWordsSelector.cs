using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudApplication.WordsSelectors
{
    public class ExcludeBoringWordsSelector : ExcludeWordsSelector
    {
        public static readonly string[] Prepositions =
        {
            "aboard", "about", "above", "across", "after", "against", "along",
            "alongside", "amid", "among", "amongst", "around", "as", "aside", "astride", "at", "atop", "barring",
            "before", "behind", "below", "beneath", "beside", "besides", "between", "beyond", "but", "by", "circa",
            "concerning", "considering", "despite", "down", "during", "except", "excepting", "excluding", "failing",
            "following", "for", "from", "in", "including", "inside", "into", "like", "minus", "near", "nearby", "next",
            "notwithstanding", "of", "off", "on", "onto", "opposite", "outside", "over", "past", "per", "plus",
            "regarding", "round", "save", "since", "than", "through", "throughout", "till", "times", "to", "toward",
            "towards", "under", "underneath", "unlike", "until", "unto", "up", "upon", "versus", "via", "with", "within",
            "without", "worth"
        };

        public static readonly string[] Pronouns =
        {
            "i", "you", "he", "she", "it", "we", "they", "me", "him", "her", "your", "our", "their", "its",
            "us", "them", "who", "her", "it", "whom", "mine", "yours", "his", "hers", "ours", "theirs", "this", "that",
            "these", "those", "which", "what", "whose", "whoever", "whatever", "whichever", "whomever", "myself",
            "yourself", "himself", "herself", "itself", "ourselves", "themselves", "anything", "everybody", "another",
            "each", "few", "many", "none", "some", "all", "any", "anybody", "anyone", "everyone", "everything", "no",
            "one", "nobody", "nothing", "none", "other", "others", "several", "somebody", "someone", "something", "most",
            "enough", "little", "more", "both", "either", "neither", "one", "much", "such"
        };

        public static readonly string[] Interjections =
        {
            "aha", "ahem", "ahh", "ahoy", "alas", "arg", "aw", "bam", "bingo", "blah", "boo", "bravo", "brrr", "cheers",
            "congratulations", "dang", "drat", "darn", "duh", "eek", "eh", "eureka", "fiddlesticks",
            "gadzooks", "gee", "gee", "whiz", "golly", "goodbye", "goodness", "good", "grief", "gosh", "haha",
            "hallelujah", "hello", "hey", "hmm", "huh",
            "humph", "hurray", "oh", "oh", "dear", "oh", "my", "oh", "well", "oops", "ouch", "ow", "phew", "phooey",
            "pooh", "pow", "rats", "shh", "shoo", "thanks", "there", "tuttut", "uhhuh", "uhoh", "ugh", "wahoo", "well",
            "whoa", "whoops", "wow", "yeah", "yes", "yikes", "yippee", "yo", "yuck"
        };

        public static readonly string[] Conjunctions =
        {
            "after", "although", "as", "as", "if", "and", "or",
            "much", "as", "soon", "though", "because", "before", "by", "the", "even", "if",
            "even", "though", "if", "in", "that", "in", "once", "only", "if",
            "that", "since", "so", "than", "though", "till", "unless", "until", "when", "whenever",
            "where", "wherever", "while"
        };

        public static readonly string[] OtherStuff = {"not", "a", "an", "the", "is", "be", "have", "do", "yet", "already", "are", "was"};

        public static readonly string[] AllBoringWords =
            Prepositions
                .Concat(Pronouns)
                .Concat(Interjections)
                .Concat(Conjunctions)
                .Concat(OtherStuff)
                .ToArray();

        public ExcludeBoringWordsSelector() : base(AllBoringWords)
        {
        }
    }
}
