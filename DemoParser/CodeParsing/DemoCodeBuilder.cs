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
        private readonly FileHashCalculator _hashCalculator = new FileHashCalculator();
        private readonly StringBuilder _outputCode = new StringBuilder();
        private readonly Output _outputDemo = new Output();

        private readonly string _filePath;
        private readonly CodeSlicer _codeSlicer;
        private readonly RegionContainer _regions;

        private DemoCodeBuilder(string filePath, List<CodeRegion> regions)
        {
            _filePath = filePath;
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

        public DemoCodeBuilder SetFileHash()
        {
            var hash = _hashCalculator.Get(_filePath);
            _outputDemo.FileHash = hash;
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

        private void CopyWalkthroughs(List<LinesRange> walkthroughRanges)
        {
            var walkRegions = _regions.Walk;

            for (var i = 0; i < walkRegions.Count; i++)
            {
                var range = walkthroughRanges[i];

                var walkthrough = new WalkthroughOutput
                {
                    Range = range,
                    RegionName = walkRegions[i].Name
                };

                _outputDemo.Walkthroughs.Add(walkthrough);
            }
        }

        public Output Build()
        {
            _outputDemo.SourceCode = _outputCode.ToString();
            return _outputDemo;
        }

        public class Output
        {
            public Output()
            {
                Walkthroughs = new List<WalkthroughOutput>();
            }

            public string SourceCode { get; set; }
            public int UsingsLastLine { get; set; }
            public List<WalkthroughOutput> Walkthroughs { get; set; }
            public string FileHash { get; set; }
        }

        public class WalkthroughOutput
        {
            public LinesRange Range { get; set; }
            public string RegionName { get; set; }
        }
    }
}
