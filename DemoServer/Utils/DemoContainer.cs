using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DemoParser;
using DemoParser.Models;
using DemoSrc.CSharp;

namespace DemoServer.Utils
{
    internal static class DemoContainer
    {
        private static readonly List<DemoLanguage> SupportedLanguages = new List<DemoLanguage>
        {
            DemoLanguage.CSharp
        };

        private static readonly Assembly SourceCodeAssembly = typeof(DemoCodeController).Assembly;

        private static readonly List<ControllerTypeEntry> ControllerTypes = new List<ControllerTypeEntry>();
        public static readonly Dictionary<DemoLanguage, List<DemoCategory>> Categories = new Dictionary<DemoLanguage, List<DemoCategory>>();

        private static string _demoSourceFolder;

        public static void Initialize(string demoSourceFolder)
        {
            _demoSourceFolder = demoSourceFolder;

            foreach (var language in SupportedLanguages)
            {
                var parserSettings = new ParserSettings
                {
                    RootSourceFolder = _demoSourceFolder,
                    Language = language
                };

                var parser = new Parser(parserSettings);
                var result = parser.Run();

                var categories = result.ToList();
                Categories[language] = categories;

                if (language != DemoLanguage.CSharp)
                    continue;

                foreach (var category in categories)
                    AddControllersFor(category);
            }
        }

        private static void AddControllersFor(DemoCategory category)
        {
            foreach (var demo in category.Demos)
            {
                var entry = new ControllerTypeEntry
                {
                    Category = category.FolderName,
                    DemoName = demo.Name
                };

                var type = SourceCodeAssembly.GetType(demo.ControllerFullName, true);

                entry.ControllerType = type;
                ControllerTypes.Add(entry);
            }
        }

        private class ControllerTypeEntry
        {
            public string Category { get; set; }
            public string DemoName { get; set; }
            public Type ControllerType { get; set; }
        }
    }
}
