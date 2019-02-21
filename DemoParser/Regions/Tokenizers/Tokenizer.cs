using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DemoParser.Utils;

namespace DemoParser.Regions.Tokenizers
{
    public class Tokenizer
    {
        private readonly Regex _regionStartFinder;
        private readonly Regex _regionEndFinder;

        public Tokenizer(Regex regionStartFinder, Regex regionEndFinder)
        {
            _regionStartFinder = regionStartFinder;
            _regionEndFinder = regionEndFinder;
        }

        public IEnumerable<RegionToken> GetFromFile(string filePath)
        {
            if (File.Exists(filePath) == false)
                throw new ParsingException($"File {filePath} does not exist.");

            var lines = File.ReadLines(filePath);
            var lineCnt = 0;
            var depth = 0;

            foreach (var line in lines)
            {
                lineCnt++;
                var token = GetRegionStartMatch(line) ?? GetRegionEndMatch(line);

                if (token == null)
                    continue;

                if (token.Type == TokenType.End)
                    depth--;

                token.LineNumber = lineCnt;
                token.Depth = depth;
                yield return token;

                if (token.Type == TokenType.Start)
                    depth++;
            }
        }

        private RegionToken GetRegionStartMatch(string line)
        {
            var match = _regionStartFinder.Match(line);

            if (!match.Success)
                return null;

            var captureGroup = match.Groups[1];
            var regionName = captureGroup.Value;

            return new RegionToken
            {
                Name = regionName,
                Type = TokenType.Start
            };
        }

        private RegionToken GetRegionEndMatch(string line)
        {
            var match = _regionEndFinder.Match(line);

            return match.Success
                ? new RegionToken { Type = TokenType.End }
                : null;
        }
    }
}
