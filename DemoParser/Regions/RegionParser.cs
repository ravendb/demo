using System;
using System.Collections.Generic;
using System.Linq;
using DemoParser.Regions.Tokenizers;

namespace DemoParser.Regions
{
    public class RegionParser
    {
        private readonly string _filePath;
        private readonly Tokenizer _tokenizer;

        public RegionParser(string filePath, Tokenizer tokenizer)
        {
            _filePath = filePath;
            _tokenizer = tokenizer;
        }

        private List<RegionToken> _tokens;

        public IEnumerable<CodeRegion> GetRegions()
        {
            _tokens = _tokenizer.GetFromFile(_filePath).ToList();
            var regions = TraverseTokens();
            return regions;
        }

        private IEnumerable<CodeRegion> TraverseTokens()
        {
            for (var i = 0; i < _tokens.Count; i++)
            {
                var token = _tokens[i];

                if (token.Type == TokenType.End)
                    continue;

                yield return GetRegionForStartToken(token, i);
            }
        }

        private CodeRegion GetRegionForStartToken(RegionToken token, int tokenNum)
        {
            var region = new CodeRegion
            {
                Name = token.Name,
                LineStart = token.LineNumber,
                Depth = token.Depth
            };

            var endTokenNum = GetEndTokenNumForStart(tokenNum);
            var endToken = _tokens[endTokenNum];

            region.LineEnd = endToken.LineNumber;

            return region;
        }

        private int GetEndTokenNumForStart(int startNum)
        {
            var startToken = _tokens[startNum];

            if (startToken.Type == TokenType.End)
                throw new InvalidOperationException($"Unexpected token type for line {startToken.LineNumber}");

            var expectedDepth = startToken.Depth;

            for (var i = startNum + 1; i < _tokens.Count; i++)
            {
                var nextToken = _tokens[i];
                if (nextToken.Depth == expectedDepth && nextToken.Type == TokenType.End)
                    return i;
            }

            throw new InvalidOperationException($"Could not find closing token for line {startToken.LineNumber}");
        }
    }
}
