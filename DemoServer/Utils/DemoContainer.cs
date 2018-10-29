using System;
using System.Collections.Generic;
using System.Linq;
using DemoParser;
using DemoParser.Models;
using Microsoft.Extensions.Logging;

namespace DemoServer.Utils
{
    public class DemoContainer
    {
        private readonly ILogger _logger;

        public readonly Dictionary<DemoLanguage, List<DemoCategory>> Categories =
            new Dictionary<DemoLanguage, List<DemoCategory>>();

        private const DemoLanguage SupportedLanguage = DemoLanguage.CSharp;

        private DemoContainer(string demoSourceFolder, ILogger<DemoContainer> logger)
        {
            _logger = logger;
            var language = SupportedLanguage;

            var parserSettings = new ParserSettings
            {
                SourceCodeFolder = demoSourceFolder,
                Language = language
            };

            var parser = new Parser(parserSettings);

            try
            {
                var result = parser.Run();
                Categories[language] = result.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during source code parsing.");
            }
        }

        public static DemoContainer Initialize(string demoSourceFolder, ILogger<DemoContainer> logger)
        {
            return new DemoContainer(demoSourceFolder, logger);
        }

        public Demo GetDemo(string categoryName, string demoName)
        {
            var categoriesForLanguage = Categories[SupportedLanguage];

            var category = categoriesForLanguage.SingleOrDefault(x =>
                string.Equals(x.Slug, categoryName, StringComparison.OrdinalIgnoreCase));

            if (category == null)
                throw new InvalidOperationException($"Category {categoryName} was not found.");

            var demo = category.Demos.SingleOrDefault(x =>
                string.Equals(x.Slug, demoName, StringComparison.OrdinalIgnoreCase));

            if (demo == null)
                throw new InvalidOperationException($"Demo {demoName} was not found in category {categoryName}.");

            return demo;
        }
    }
}
