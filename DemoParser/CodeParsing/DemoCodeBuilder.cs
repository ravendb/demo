using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoParser.Models;
using DemoParser.Regions;
using DemoParser.Utils;

namespace DemoParser.CodeParsing
{
    public class DemoCodeBuilder
    {
        private readonly StringBuilder _outputCode = new StringBuilder();
        private readonly Demo _outputDemo = new Demo();
        private readonly CodeSlicer _codeSlicer;

        private readonly RegionContainer _regions;

        private DemoCodeBuilder(string filePath, List<CodeRegion> regions)
        {
            _regions = new RegionContainer(regions);
            _codeSlicer = new CodeSlicer(filePath, _regions);
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

            var usingsCode = _codeSlicer.CopyUsings(new LinesRange
            {
                Start = usingsRegion.LineStart,
                End = usingsRegion.LineEnd
            });

            _outputCode.Append(usingsCode.Code);
            _outputDemo.UsingsLastLine = usingsCode.LineCount;
            return this;
        }

        public DemoCodeBuilder SetDemoBody()
        {
            var demoRegion = _regions.Demo;

            if (demoRegion == null)
                throw new InvalidOperationException($"Region {RegionNames.Demo} was not found.");

            var demoCode = _codeSlicer.CopyCodeWithWalkthroughs(new CodeSlicer.CodeWithWalkthroughsInput
            {
                Start = demoRegion.LineStart,
                End = demoRegion.LineEnd,
                LineCountOffset = _outputDemo.UsingsLastLine
            });

            _outputCode.Append(demoCode.Code);

            CopyWalkthroughs(demoCode.WalkthroughRanges);
            return this;
        }

        private void CopyWalkthroughs(IEnumerable<LinesRange> walkthroughRanges)
        {
            foreach (var range in walkthroughRanges)
            {
                _outputDemo.Walkthroughs.Add(new DemoWalkthrough
                {
                    Lines = range
                });
            }
        }

        public Demo Build()
        {
            _outputDemo.SourceCode = _outputCode.ToString();
            return _outputDemo;
        }
    }
}
