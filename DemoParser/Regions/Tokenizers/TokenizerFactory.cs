using System.Collections.Generic;
using System.Text.RegularExpressions;
using DemoParser.Models;

namespace DemoParser.Regions.Tokenizers
{
    public static class TokenizerFactory
    {
        private static readonly Dictionary<DemoLanguage, Regex> RegionStartFinders = new Dictionary<DemoLanguage, Regex>
        {
            {DemoLanguage.CSharp, new Regex(@"#region\s*([a-zA-z0-9]*)")},
            {DemoLanguage.Java, new Regex(@"//region\s*([a-zA-z0-9]*)")}
        };

        private static readonly Dictionary<DemoLanguage, Regex> RegionEndFinders = new Dictionary<DemoLanguage, Regex>
        {
            {DemoLanguage.CSharp, new Regex(@"#endregion")},
            {DemoLanguage.Java, new Regex(@"//endregion")}
        };

        public static Tokenizer GetFor(DemoLanguage language)
        {
            var startFinder = RegionStartFinders[language];
            var endFinder = RegionEndFinders[language];

            return new Tokenizer(startFinder, endFinder);
        }
    }
}
