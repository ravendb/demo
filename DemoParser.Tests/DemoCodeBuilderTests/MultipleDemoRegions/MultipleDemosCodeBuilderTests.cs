using DemoParser.CodeParsing;
using Xunit;

namespace DemoParser.Tests.DemoCodeBuilderTests.MultipleDemoRegions
{
    public class MultipleDemosCodeBuilderTests : DemoCodeBuilderTests
    {
        private DemoCodeBuilder GetBuilder() => GetBuilder("DemoCodeBuilderTests\\MultipleDemoRegions\\Input.cs");
        private string GetExpectedOutput() => GetExpectedOutput("DemoCodeBuilderTests\\MultipleDemoRegions\\ExpectedOutput.txt");

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

            var expectedOutput = GetExpectedOutput();

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
            Assert.Equal(7, resultWalkthroughs.Count);

            Assert.All(resultWalkthroughs, w => Assert.NotEmpty(w.RegionName));
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
                    var aLines = resultWalkthroughs[i].Range;
                    var bLines = resultWalkthroughs[i + 1].Range;

                    Assert.True(aLines.Start <= bLines.Start, $"Walkthrough #{i} start is larger than walkthrough #{i + 1} start.");
                    Assert.True(aLines.End <= bLines.End, $"Walkthrough #{i} end is larger than walkthrough #{i + 1} end.");
                }
            }
        }

        [Fact]
        public void CalculatesFileHash()
        {
            var result = GetBuilder()
                .SetFileHash()
                .Build();

            Assert.NotNull(result);
            Assert.NotEmpty(result.FileHash);
        }
    }
}
