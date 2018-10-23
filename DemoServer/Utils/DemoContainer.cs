using System.Collections.Generic;
using System.Linq;
using DemoParser;
using DemoParser.Models;

namespace DemoServer.Utils
{
    internal static class DemoContainer
    {
        public static readonly Dictionary<DemoLanguage, List<DemoCategory>> Categories =
            new Dictionary<DemoLanguage, List<DemoCategory>>();

        private static string _demoSourceFolder;

        public static void Initialize(string demoSourceFolder)
        {
            _demoSourceFolder = demoSourceFolder;
            var language = DemoLanguage.CSharp;

            var parserSettings = new ParserSettings
            {
                SourceCodeFolder = _demoSourceFolder,
                Language = language
            };

            var parser = new Parser(parserSettings);
            var result = parser.Run();

            var categories = result.ToList();
            Categories[language] = categories;
        }
    }
}
