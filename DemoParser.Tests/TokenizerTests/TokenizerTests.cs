using DemoParser.Regions.Tokenizers;
using Xunit;

namespace DemoParser.Tests.TokenizerTests
{
    public abstract class TokenizerTests
    {
        protected bool IsStartToken(RegionToken token) => token.Type == TokenType.Start;
        protected bool IsEndToken(RegionToken token) => token.Type == TokenType.End;

        protected void AssertDoesNotContainWhitespace(string s)
        {
            Assert.All(s, c => Assert.False(char.IsWhiteSpace(c)));
        }
    }
}
