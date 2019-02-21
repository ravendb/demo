using System.Collections.Generic;
using System.IO;
using System.Linq;
using DemoParser.Models;
using DemoParser.Regions.Tokenizers;
using Xunit;

namespace DemoParser.Tests.TokenizerTests.Csharp
{
    public class CsharpTokenizerTests : TokenizerTests
    {
        private const string StartRegionTag = "#region";
        private const string EndRegionTag = "#endregion";

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

            Assert.All(startTokens, x => Assert.DoesNotContain(StartRegionTag, x.Name));
            Assert.All(startTokens, x => Assert.DoesNotContain(EndRegionTag, x.Name));
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

        private static readonly string DefaultFilePath = Path.Join("TokenizerTests", "Csharp", "Input.cs");

        private List<RegionToken> Act() => Act(DefaultFilePath);

        private List<RegionToken> Act(string path)
        {
            var tokenizer = TokenizerFactory.GetFor(DemoLanguage.CSharp);
            return tokenizer.GetFromFile(path).ToList();
        }
    }
}
