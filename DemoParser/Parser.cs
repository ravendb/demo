using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DemoParser.CodeParsing;
using DemoParser.Models;
using DemoParser.Utils;
using DemoParser.Utils.Json;

namespace DemoParser
{
    public class Parser
    {
        private readonly ParserSettings _settings;
        private readonly JsonImporter _jsonImporter = new JsonImporter();

        public Parser(ParserSettings settings)
        {
            _settings = settings;
        }

        public IEnumerable<DemoCategory> Run()
        {
            var rootFolder = _settings.SourceCodeFolder;

            var jsonDemos = _jsonImporter.GetDemoSets(rootFolder);

            foreach (var demoSet in jsonDemos)
            {
                var category = demoSet.Category;
                var categoryFolder = Path.Join(rootFolder, category.Directory);
                yield return ParseCategory(categoryFolder, demoSet);
            }
        }

        private DemoCategory ParseCategory(string categoryFolder, JsonDemoSet demoSet)
        {
            var demos = GetDemos(categoryFolder, demoSet.Demos).ToList();
            var category = demoSet.Category;
            var folderName = Path.GetFileName(categoryFolder);

            return new DemoCategory
            {
                Slug = category.Slug.ToLower(),
                Directory = folderName,
                Demos = demos,
                Title = category.Title
            };
        }

        private IEnumerable<Demo> GetDemos(string categoryFolder, IEnumerable<string> demoNames)
        {
            foreach (var demoName in demoNames)
            {
                var demoFolder = Path.Join(categoryFolder, demoName);
                yield return ParseDemo(demoFolder);
            }
        }

        private Demo ParseDemo(string demoFolder)
        {
            var demoJson = _jsonImporter.GetMetadata(demoFolder);
            var codeFilePath = Path.Join(demoFolder, demoJson.SourceFileName);
            var demo = demoJson.ToDemo();

            var codeOutput = DemoCodeBuilder.Initialize(codeFilePath)
                .SetUsings()
                .SetDemoBody()
                .SetFileHash()
                .Build();

            var merger = new DemoMerger(codeOutput);

            try
            {
                merger.MergeTo(demo);
            }
            catch (Exception e)
            {
                throw new ParsingException(
                    $"An exception occurred when parsing {codeFilePath}. Check inner exception for details.", e);
            }
            return demo;
        }
    }
}
