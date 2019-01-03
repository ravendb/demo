using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DemoParser.Models;
using DemoParser.Regions;

namespace DemoParser.CodeParsing
{
    internal class CodeSlicer
    {
        private readonly string[] _inputLines;
        private readonly RegionContainer _regions;
        private readonly Dictionary<string, LinesRange> _walkthroughRanges = new Dictionary<string, LinesRange>();

        private int _bodyWhitespaceOffset;

        public CodeSlicer(string filePath, RegionContainer regions)
        {
            _inputLines = File.ReadAllLines(filePath);
            _regions = regions;
        }

        public UsingsOutput CopyUsings(LinesRange range)
        {
            var usingsRegion = _regions.Usings;

            if (usingsRegion == null)
                return null;

            var startLine = range.Start;
            var endLine = range.End;
            var outputBuilder = new StringBuilder();
            var lineCount = 0;

            for (var i = startLine + 1; i < endLine; i++, lineCount++)
            {
                var line = GetCodeLine(i);
                outputBuilder.AppendLine(line);
            }

            return new UsingsOutput
            {
                Code = outputBuilder.ToString(),
                LineCount = lineCount
            };
        }

        public CodeWithWalkthroughsOutput CopyCodeWithWalkthroughs(CodeWithWalkthroughsInput parameters)
        {
            var outputCode = new StringBuilder();

            var startLine = parameters.Start;
            var endLine = parameters.End;
            var outputLineCnt = parameters.LineCountOffset;

            _bodyWhitespaceOffset = GetWhitespaceOffsetForLine(startLine + 1);

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

                var line = CopyCodeLine(i);
                outputCode.AppendLine(line);
                outputLineCnt++;
            }

            return new CodeWithWalkthroughsOutput
            {
                Code = outputCode.ToString(),
                WalkthroughRanges = _walkthroughRanges.Values.ToList()
            };
        }

        private int GetWhitespaceOffsetForLine(int lineNum)
        {
            var line = GetCodeLine(lineNum);
            var i = 0;

            while (i < line.Length && char.IsWhiteSpace(line[i]))
                i++;

            return i;
        }

        private string GetCodeLine(int num) => _inputLines[num - 1];

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

        private string CopyCodeLine(int num)
        {
            var line = GetCodeLine(num);
            var withoutLeadingWhitespace = TrimLeadingWhitespaces(line);
            return withoutLeadingWhitespace;
        }

        private string TrimLeadingWhitespaces(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return line;

            var i = 0;
            while (i < line.Length && i < _bodyWhitespaceOffset && char.IsWhiteSpace(line[i]))
                i++;

            return line.Substring(i);
        }

        internal class UsingsOutput
        {
            public string Code { get; set; }
            public int LineCount { get; set; }
        }

        internal class CodeWithWalkthroughsInput : LinesRange
        {
            public int LineCountOffset { get; set; }
        }

        internal class CodeWithWalkthroughsOutput
        {
            public CodeWithWalkthroughsOutput()
            {
                WalkthroughRanges = new List<LinesRange>();
            }

            public string Code { get; set; }
            public List<LinesRange> WalkthroughRanges { get; set; }

            public int LineCount => Code.Split(Environment.NewLine).Length;
        }
    }
}
