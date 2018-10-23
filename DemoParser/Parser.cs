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
            var languageFolder = LanguageMaps.GetFolderFor(_settings.Language);
            var rootFolder = Path.Join(_settings.RootSourceFolder, languageFolder);

            var categories = _jsonImporter.GetCategories(rootFolder);

            foreach (var categoryJson in categories)
            {
                var categoryFolder = Path.Join(rootFolder, categoryJson.FolderName);
                yield return ParseCategory(categoryFolder, categoryJson);
            }
        }

        private DemoCategory ParseCategory(string categoryFolder, JsonCategory category)
        {
            var demos = GetDemos(categoryFolder).ToList();
            var folderName = Path.GetFileName(categoryFolder);

            return new DemoCategory
            {
                Title = category.Title,
                FolderName = folderName,
                Demos = demos
            };
        }

        private IEnumerable<Demo> GetDemos(string categoryFolder)
        {
            var demoList = _jsonImporter.GetDemos(categoryFolder);

            foreach (var demoName in demoList)
            {
                var demoFolder = Path.Join(categoryFolder, demoName);
                yield return ParseDemo(demoFolder);
            }
        }

        private Demo ParseDemo(string demoFolder)
        {
            var demo = _jsonImporter.GetMetadata(demoFolder);
            var codeFileName = LanguageMaps.GetFileNameFor(_settings.Language);
            var codeFilePath = Path.Join(demoFolder, codeFileName);

            var codeOutput = DemoCodeBuilder.Initialize(codeFilePath)
                .SetUsings()
                .SetDemoBody()
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
