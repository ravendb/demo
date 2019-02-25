using System;
using System.Collections.Generic;
using System.Linq;
using DemoParser;
using DemoParser.Models;
using Microsoft.Extensions.Logging;

namespace DemoServer.Utils.Demos
{
    public class DemoContainer
    {
        private const DemoLanguage DefaultLanguage = DemoLanguage.CSharp;

        private readonly ILogger _logger;
        private readonly Settings _settings;

        public readonly Dictionary<DemoLanguage, List<DemoCategory>> CategoriesForLanguages =
            new Dictionary<DemoLanguage, List<DemoCategory>>();

        private DemoContainer(Dictionary<DemoLanguage, string> demoLanguageSources, ILogger<DemoContainer> logger, Settings settings)
        {
            _logger = logger;
            _settings = settings;

            foreach ((DemoLanguage language, string sourceFolder) in demoLanguageSources)
            {
                ParseLanguage(language, sourceFolder);
            }
        }

        private void ParseLanguage(DemoLanguage language, string sourceFolder)
        {
            var parserSettings = new ParserSettings
            {
                SourceCodeFolder = sourceFolder,
                Language = language
            };

            var parser = new Parser(parserSettings);

            try
            {
                var result = parser.Run();
                CategoriesForLanguages[language] = result.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during source code parsing.");
            }
        }

        public static DemoContainer Initialize(Dictionary<DemoLanguage, string> demoLanguageSources, ILogger<DemoContainer> logger, Settings settings) =>
            new DemoContainer(demoLanguageSources, logger, settings);

        public Demo GetDemo(DemoLanguage language, string categoryName, string demoName)
        {
            if (CategoriesForLanguages.ContainsKey(language) == false)
                throw new InvalidOperationException($"There are no categories for language: {language}.");

            var categoriesForLanguage = CategoriesForLanguages[language];

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

        private IEnumerable<Demo> GetDemosForCategory(DemoCategory category) => category.Demos
            .Where(x => _settings.ConferenceMode || x.ConferenceOnly == false);

        public List<CategoriesForLanguage> GetCategoriesForLanguages() => CategoriesForLanguages.Keys.Select(GetCategoriesForLanguage).ToList();

        private CategoriesForLanguage GetCategoriesForLanguage(DemoLanguage language)
        {
            var categories = CategoriesForLanguages[language];
            var categoriesForLanguages = categories.Select(ToCategoryForLanguage).ToList();

            return new CategoriesForLanguage
            {
                Language = language,
                Categories = categoriesForLanguages
            };
        }

        private CategoryForLanguage ToCategoryForLanguage(DemoCategory category)
        {
            var demos = GetDemosForCategory(category)
                .Select(ToDemoForLanguage)
                .ToList();

            return new CategoryForLanguage
            {
                Slug = category.Slug,
                Demos = demos
            };
        }

        private DemoForLanguage ToDemoForLanguage(Demo demo) => new DemoForLanguage
        {
            Slug = demo.Slug,
            Title = demo.Title
        };

        public List<CategoryWithDemoVersions> GetCategoriesWithVersions()
        {
            var categories = CategoriesForLanguages[DefaultLanguage];

            return categories.Select(ToCategoryWithVersions).ToList();
        }

        private CategoryWithDemoVersions ToCategoryWithVersions(DemoCategory category)
        {
            var demos = GetDemosForCategory(category)
                .Select(ToDemoWithVersion)
                .ToList();

            return new CategoryWithDemoVersions
            {
                Slug = category.Slug,
                Title = category.Title,
                Demos = demos
            };
        }

        private DemoWithVersion ToDemoWithVersion(Demo demo) => new DemoWithVersion
        {
            Title = demo.Title,
            Slug = demo.Slug,
            Hash = demo.Hash
        };
    }
}
