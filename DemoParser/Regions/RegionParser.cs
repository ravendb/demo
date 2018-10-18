using System;
using System.Collections.Generic;
using System.Linq;
using TokenType = DemoParser.Regions.Tokenizer.RegionToken.TokenType;

namespace DemoParser.Regions
{
    public class RegionParser
    {
        private readonly Tokenizer _tokenizer = new Tokenizer();
        private readonly CodeSlicer _codeSlicer = new CodeSlicer();

        private readonly string _filePath;

        public RegionParser(string filePath)
        {
            _filePath = filePath;
        }

        private List<Tokenizer.RegionToken> _tokens;

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

        private CodeRegion GetRegionForStartToken(Tokenizer.RegionToken token, int tokenNum)
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
            SetRegionContent(region);

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

        private void SetRegionContent(CodeRegion region)
        {
            var code = _codeSlicer.GetSliceFor(_filePath, region);
            region.Content = code;
        }
    }
}
