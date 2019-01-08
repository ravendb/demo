using System.IO;
using DemoParser.CodeParsing;
using Xunit;

namespace DemoParser.Tests.DemoCodeBuilderTests
{
    public abstract class DemoCodeBuilderTests
    {
        protected DemoCodeBuilder GetBuilder(string filePath) => DemoCodeBuilder.Initialize(filePath);

        protected string GetExpectedOutput(string filePath) => File.ReadAllText(filePath);

        protected void AsserWalkthroughRange(int start, int end, DemoCodeBuilder.WalkthroughOutput walkthrough)
        {
            var range = walkthrough.Range;

            Assert.Equal(start, range.Start);
            Assert.Equal(end, range.End);
        }
    }
}
