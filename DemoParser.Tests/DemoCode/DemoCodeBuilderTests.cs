using System.IO;
using DemoParser.CodeParsing;
using Xunit;

namespace DemoParser.Tests.DemoCode
{
    public class DemoCodeBuilderTests
    {
        private const string DefaultFilePath = "DemoCode\\Input.cs";
        private const string DefaultExpectedOutputFilePath = "DemoCode\\ExpectedOutput.txt";

        [Fact]
        public void BuildsUsings()
        {
            var result = GetBuilder()
                .SetUsings()
                .Build();

            Assert.NotNull(result);
            Assert.True(result.UsingsLastLine > 0);
            Assert.False(string.IsNullOrWhiteSpace(result.SourceCode));
        }

        [Fact]
        public void BuildsDemoBody()
        {
            var result = GetBuilder()
                .SetUsings()
                .SetDemoBody()
                .Build();

            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result.SourceCode));

            var expectedOutput = File.ReadAllText(DefaultExpectedOutputFilePath);

            Assert.Equal(expectedOutput, result.SourceCode);
        }

        [Fact]
        public void SetsWalkthroughs()
        {
            var result = GetBuilder()
                .SetUsings()
                .SetDemoBody()
                .Build();

            var resultWalkthroughs = result.Walkthroughs;

            Assert.NotEmpty(resultWalkthroughs);
            Assert.Equal(5, resultWalkthroughs.Count);
        }

        [Fact]
        public void SetsWalkthroughs_Ordered()
        {
            var result = GetBuilder()
                .SetUsings()
                .SetDemoBody()
                .Build();

            var resultWalkthroughs = result.Walkthroughs;

            for (var i = 0; i < resultWalkthroughs.Count; i++)
            {
                if (i < resultWalkthroughs.Count - 1)
                {
                    var aLines = resultWalkthroughs[i].Lines;
                    var bLines = resultWalkthroughs[i + 1].Lines;

                    Assert.True(aLines.Start <= bLines.Start, $"Walkthrough #{i} start is larger than walkthrough #{i+1} start.");
                    Assert.True(aLines.End <= bLines.End, $"Walkthrough #{i} end is larger than walkthrough #{i + 1} end.");
                }
            }
        }

        private DemoCodeBuilder GetBuilder(string filePath = DefaultFilePath)
        {
            return DemoCodeBuilder.Initialize(filePath);
        }
    }
}
