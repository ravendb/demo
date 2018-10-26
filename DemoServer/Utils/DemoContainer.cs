using System.Collections.Generic;
using System.Linq;
using DemoParser;
using DemoParser.Models;

namespace DemoServer.Utils
{
    public class DemoContainer
    {
        public readonly Dictionary<DemoLanguage, List<DemoCategory>> Categories =
            new Dictionary<DemoLanguage, List<DemoCategory>>();

        private DemoContainer(string demoSourceFolder)
        {
            var language = DemoLanguage.CSharp;

            var parserSettings = new ParserSettings
            {
                SourceCodeFolder = demoSourceFolder,
                Language = language
            };

            var parser = new Parser(parserSettings);
            var result = parser.Run();

            Categories[language] = result.ToList();
        }

        public static DemoContainer Initialize(string demoSourceFolder)
        {
            return new DemoContainer(demoSourceFolder);
        }
    }
}
