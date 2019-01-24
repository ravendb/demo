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
        private readonly Settings _settings;

        public readonly Dictionary<DemoLanguage, List<DemoCategory>> Categories =
            new Dictionary<DemoLanguage, List<DemoCategory>>();

        private const DemoLanguage SupportedLanguage = DemoLanguage.CSharp;

        private DemoContainer(string demoSourceFolder, ILogger<DemoContainer> logger, Settings settings)
        {
            _logger = logger;
            _settings = settings;

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

        public static DemoContainer Initialize(string demoSourceFolder, ILogger<DemoContainer> logger, Settings settings) =>
            new DemoContainer(demoSourceFolder, logger, settings);

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

        public List<CategoryResult> GetCategories()
        {
            var categoriesForLanguage = Categories[SupportedLanguage];

            var results = categoriesForLanguage.Select(ToCategoryResult).ToList();
            return results;
        }

        private CategoryResult ToCategoryResult(DemoCategory category)
        {
            var conferenceMode = _settings.ConferenceMode;

            var demos = category.Demos
                .Where(x => conferenceMode || x.ConferenceOnly == false)
                .Select(ToDemoResult)
                .ToList();

            return new CategoryResult
            {
                Slug = category.Slug,
                Title = category.Title,
                Demos = demos
            };
        }

        private DemoResult ToDemoResult(Demo demo) => new DemoResult
        {
            Slug = demo.Slug,
            Hash = demo.Hash,
            Title = demo.Title
        };

        public class CategoryResult
        {
            public string Slug { get; set; }
            public string Title { get; set; }

            public List<DemoResult> Demos { get; set; }
        }

        public class DemoResult
        {
            public string Slug { get; set; }
            public string Title { get; set; }
            public string Hash { get; set; }
        }
    }
}
