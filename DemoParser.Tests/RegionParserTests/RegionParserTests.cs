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

        [Fact]
        public void MultipleDemos_RetrievesMultipleRegions()
        {
            var result = ActOnMultipleDemoRegions();

            Assert.NotEmpty(result);
            Assert.True(result.Count > 1);
        }

        [Fact]
        public void MultipleDemos_AllRegions_ContainName()
        {
            var result = ActOnMultipleDemoRegions();

            Assert.All(result, x => Assert.False(string.IsNullOrEmpty(x.Name)));
        }

        [Fact]
        public void MultipleDemos_AllRegions_ContainValidRanges()
        {
            var result = ActOnMultipleDemoRegions();

            Assert.All(result, x => Assert.True(x.LineStart > 0));
            Assert.All(result, x => Assert.True(x.LineEnd > 0));
            Assert.All(result, x => Assert.True(x.LineStart < x.LineEnd));
        }

        [Fact]
        public void MultipleDemos_RetrievesExpectedSectionNames()
        {
            var result = ActOnMultipleDemoRegions();

            Assert.Contains(result, x => x.Name == "Usings");
            Assert.Contains(result, x => x.Name == "Demo");
            Assert.Contains(result, x => x.Name.StartsWith("Step_"));
        }

        [Fact]
        public void MultipleDemos_RetrievesWalkthroughs()
        {
            var result = ActOnMultipleDemoRegions();

            Assert.Contains(result, x => x.Name.StartsWith("Step_"));
        }

        [Fact]
        public void MultipleDemos_RetrievesMultipleDemoRegions()
        {
            var result = ActOnMultipleDemoRegions();

            var demoRegions = result.Where(x => x.Name == "Demo").ToList();
            Assert.Equal(3, demoRegions.Count);
        }

        private const string DefaultFilePath = "RegionParserTests\\Input.cs";
        private const string MultipleDemoRegionsPath = "RegionParserTests\\MultipleDemoRegionsInput.cs";

        private List<CodeRegion> Act(string filePath = DefaultFilePath)
        {
            var parser = new RegionParser(filePath);
            var result = parser.GetRegions();
            return result.ToList();
        }

        private List<CodeRegion> ActOnMultipleDemoRegions() => Act(MultipleDemoRegionsPath);
    }
}
