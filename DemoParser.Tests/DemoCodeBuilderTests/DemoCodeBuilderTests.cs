using System.IO;
using DemoParser.CodeParsing;
using DemoParser.Models;
using Xunit;

namespace DemoParser.Tests.DemoCodeBuilderTests
{
    public abstract class DemoCodeBuilderTests
    {
        protected DemoCodeBuilder GetBuilder(string filePath, DemoLanguage language)
            => DemoCodeBuilder.Initialize(filePath, language);

        protected string GetExpectedOutput(string filePath) => File.ReadAllText(filePath);

        protected void AsserWalkthroughRange(int start, int end, DemoCodeBuilder.WalkthroughOutput walkthrough)
        {
            var range = walkthrough.Range;

            Assert.Equal(start, range.Start);
            Assert.Equal(end, range.End);
        }
    }
}
