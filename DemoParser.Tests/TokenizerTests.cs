using System.Collections.Generic;
using System.Linq;
using DemoParser.Regions;
using Xunit;

namespace DemoParser.Tests
{
    public class TokenizerTests
    {
        [Fact]
        public void GetsNonEmptyList()
        {
            var result = Act();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void EachRegionHasItsEnd()
        {
            var result = Act();

            var startCount = result.Count(IsStartToken);
            var endCount = result.Count(IsEndToken);

            Assert.Equal(startCount, endCount);
        }

        [Fact]
        public void EachStartToken_HasName()
        {
            var result = Act();
            var startTokens = result.Where(IsStartToken);

            Assert.All(startTokens, x => Assert.False(string.IsNullOrWhiteSpace(x.Name)));
        }

        [Fact]
        public void EachEndToken_HasNullName()
        {
            var result = Act();
            var endTokens = result.Where(IsEndToken);

            Assert.All(endTokens, x => Assert.True(x.Name == null));
        }

        [Fact]
        public void EachStartToken_HasName_WithoutRegionTag()
        {
            var result = Act();
            var startTokens = result.Where(IsStartToken).ToList();

            Assert.All(startTokens, x => Assert.DoesNotContain("#region", x.Name));
            Assert.All(startTokens, x => Assert.DoesNotContain("#endregion", x.Name));
        }

        [Fact]
        public void EachStartToken_HasName_WithoutWhitespace()
        {
            var result = Act();
            var startTokens = result.Where(IsStartToken);

            Assert.All(startTokens, x => AssertDoesNotContainWhitespace(x.Name));
        }

        [Fact]
        public void EachTokenDepth_HasItsMatch()
        {
            var result = Act();

            var depthCounts = new Dictionary<int, int>();

            foreach (var token in result)
            {
                var key = token.Depth;
                if (depthCounts.ContainsKey(key))
                    depthCounts[key]++;
                else
                    depthCounts[key] = 1;
            }

            Assert.All(depthCounts, pair => Assert.True(pair.Value % 2 == 0));
        }

        private const string DefaultFilePath = "MockSrc\\CSharp\\Basics\\Demo101\\Demo101Controller.cs";

        private List<Tokenizer.RegionToken> Act(string path = DefaultFilePath)
        {
            var tokenizer = new Tokenizer();
            return tokenizer.GetFromFile(path).ToList();
        }

        private bool IsStartToken(Tokenizer.RegionToken token) => token.Type == Tokenizer.RegionToken.TokenType.Start;
        private bool IsEndToken(Tokenizer.RegionToken token) => token.Type == Tokenizer.RegionToken.TokenType.End;

        private void AssertDoesNotContainWhitespace(string s)
        {
            Assert.All(s, c => Assert.False(char.IsWhiteSpace(c)));
        }
    }
}
