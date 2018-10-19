using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DemoParser.Models;
using DemoParser.Regions;
using DemoParser.Utils;

namespace DemoParser.CodeParsing
{
    public class DemoCodeBuilder
    {
        private readonly string[] _inputLines;
        private readonly StringBuilder _outputCode = new StringBuilder();
        private readonly Demo _outputDemo = new Demo();

        private readonly RegionContainer _regions;
        private readonly Dictionary<string, LinesRange> _walkthroughRanges = new Dictionary<string, LinesRange>();

        private DemoCodeBuilder(string filePath, List<CodeRegion> regions)
        {
            _inputLines = File.ReadAllLines(filePath);
            _regions = new RegionContainer(regions);
        }

        public static DemoCodeBuilder Initialize(string filePath)
        {
            var parser = new RegionParser(filePath);
            var regions = parser.GetRegions().ToList();
            return new DemoCodeBuilder(filePath, regions);
        }

        public DemoCodeBuilder SetUsings()
        {
            var usingsRegion = _regions.Usings;

            if (usingsRegion == null)
                return this;

            var lineCnt = 0;

            using (var reader = new StringReader(usingsRegion.Content))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    _outputCode.AppendLine(line);
                    lineCnt++;
                }
            }

            _outputDemo.UsingsLastLine = lineCnt;
            return this;
        }

        public DemoCodeBuilder SetDemoBody()
        {
            var demoRegion = _regions.Demo;

            if (demoRegion == null)
                throw new InvalidOperationException($"Region {RegionNames.Demo} was not found.");

            CopyDemoCode(demoRegion.LineStart, demoRegion.LineEnd);
            return this;
        }

        public DemoCodeBuilder SetWalkthroughs()
        {
            foreach (var range in _walkthroughRanges)
            {
                _outputDemo.Walkthroughs.Add(new DemoWalkthrough
                {
                    Lines = range.Value
                });
            }

            return this;
        }

        private void CopyDemoCode(int startLine, int endLine)
        {
            var outputLineCnt = _outputDemo.UsingsLastLine;

            for (var i = startLine + 1; i < endLine; i++)
            {
                var startingRegion = _regions.GetWalkRegionStartingWithLine(i);
                if (startingRegion != null)
                {
                    SetWalkStartFor(startingRegion, outputLineCnt);
                    continue;
                }

                var endingRegion = _regions.GetWalkRegionEndingWithLine(i);
                if (endingRegion != null)
                {
                    SetWalkEndFor(endingRegion, outputLineCnt);
                    continue;
                }

                CopyCodeLine(i);
                outputLineCnt++;
            }
        }

        private void SetWalkStartFor(CodeRegion region, int outputLineCnt)
        {
            _walkthroughRanges[region.Name] = new LinesRange
            {
                Start = outputLineCnt + 1
            };
        }

        private void SetWalkEndFor(CodeRegion region, int outputLineCnt)
        {
            _walkthroughRanges[region.Name].End = outputLineCnt;
        }

        private void CopyCodeLine(int num)
        {
            var line = _inputLines[num - 1];
            _outputCode.AppendLine(line);
        }

        public Demo Build()
        {
            _outputDemo.SourceCode = _outputCode.ToString();
            return _outputDemo;
        }
    }
}
