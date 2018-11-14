using System.Collections.Generic;
using System.Linq;
using DemoParser.Regions;
using Xunit;

namespace DemoParser.Tests.RegionParserTests
{
    public class RegionParserTests
    {
        [Fact]
        public void RetrievesMultipleRegions()
        {
            var result = Act();

            Assert.NotEmpty(result);
            Assert.True(result.Count > 1);
        }

        [Fact]
        public void AllRegions_ContainName()
        {
            var result = Act();

            Assert.All(result, x => Assert.False(string.IsNullOrEmpty(x.Name)));
        }

        [Fact]
        public void AllRegions_ContainValidRanges()
        {
            var result = Act();

            Assert.All(result, x => Assert.True(x.LineStart > 0));
            Assert.All(result, x => Assert.True(x.LineEnd > 0));
            Assert.All(result, x => Assert.True(x.LineStart < x.LineEnd));
        }

        [Fact]
        public void RetrievesExpectedSectionNames()
        {
            var result = Act();

            Assert.Contains(result, x => x.Name == "Usings");
            Assert.Contains(result, x => x.Name == "Demo");
            Assert.Contains(result, x => x.Name.StartsWith("Step_"));
        }

        [Fact]
        public void RetrievesWalkthroughs()
        {
            var result = Act();

            Assert.Contains(result, x => x.Name.StartsWith("Step_"));
        }

        private const string DefaultFilePath = "RegionParserTests\\Input.cs";

        private List<CodeRegion> Act(string filePath = DefaultFilePath)
        {
            var parser = new RegionParser(filePath);
            var result = parser.GetRegions();
            return result.ToList();
        }
    }
}
