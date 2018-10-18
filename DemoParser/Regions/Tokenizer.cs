using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DemoParser.Regions
{
    public class Tokenizer
    {
        private static readonly Regex RegionStartFinder = new Regex(@"#region\s*([a-zA-z0-9]*)");
        private static readonly Regex RegionEndFinder = new Regex(@"#endregion");

        public IEnumerable<RegionToken> GetFromFile(string filePath)
        {
            var lines = File.ReadLines(filePath);
            var lineCnt = 0;
            var depth = 0;

            foreach (var line in lines)
            {
                lineCnt++;
                var token = GetRegionStartMatch(line) ?? GetRegionEndMatch(line);

                if (token == null)
                    continue;

                if (token.Type == RegionToken.TokenType.End)
                    depth--;

                token.LineNumber = lineCnt;
                token.Depth = depth;
                yield return token;

                if (token.Type == RegionToken.TokenType.Start)
                    depth++;
            }
        }

        private RegionToken GetRegionStartMatch(string line)
        {
            var match = RegionStartFinder.Match(line);

            if (!match.Success)
                return null;

            var captureGroup = match.Groups[1];
            var regionName = captureGroup.Value;

            return new RegionToken
            {
                Name = regionName,
                Type = RegionToken.TokenType.Start
            };
        }

        private RegionToken GetRegionEndMatch(string line)
        {
            var match = RegionEndFinder.Match(line);

            return match.Success
                ? new RegionToken { Type = RegionToken.TokenType.End }
                : null;
        }

        public class RegionToken
        {
            public string Name { get; set; }

            public int LineNumber { get; set; }

            public TokenType Type { get; set; }

            public int Depth { get; set; }

            public enum TokenType
            {
                Start,
                End
            }
        }
    }
}
