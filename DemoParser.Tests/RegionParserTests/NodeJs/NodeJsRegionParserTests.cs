using System.Collections.Generic;
using System.IO;
using System.Linq;
using DemoParser.Models;
using DemoParser.Regions;
using DemoParser.Regions.Tokenizers;
using Xunit;

namespace DemoParser.Tests.RegionParserTests.NodeJs
{
    public class NodeJsRegionParserTests
    {
        private const string UsingsRegionName = "Usings";
        private const string DemoRegionName = "Demo";
        private const string WtStepRegionPrefix = "Step_";

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

            Assert.Contains(result, x => x.Name == UsingsRegionName);
            Assert.Contains(result, x => x.Name == DemoRegionName);
            Assert.Contains(result, x => x.Name.StartsWith(WtStepRegionPrefix));
        }

        [Fact]
        public void RetrievesWalkthroughs()
        {
            var result = Act();

            Assert.Contains(result, x => x.Name.StartsWith(WtStepRegionPrefix));
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

            Assert.Contains(result, x => x.Name == UsingsRegionName);
            Assert.Contains(result, x => x.Name == DemoRegionName);
            Assert.Contains(result, x => x.Name.StartsWith(WtStepRegionPrefix));
        }

        [Fact]
        public void MultipleDemos_RetrievesWalkthroughs()
        {
            var result = ActOnMultipleDemoRegions();

            Assert.Contains(result, x => x.Name.StartsWith(WtStepRegionPrefix));
        }

        [Fact]
        public void MultipleDemos_RetrievesMultipleDemoRegions()
        {
            var result = ActOnMultipleDemoRegions();

            var demoRegions = result.Where(x => x.Name == DemoRegionName).ToList();
            Assert.Equal(3, demoRegions.Count);
        }

        private static readonly string DefaultFilePath = Path.Join("RegionParserTests", "NodeJs", "input.js");
        private static readonly string MultipleDemoRegionsPath = Path.Join("RegionParserTests", "NodeJs", "multipleDemoRegionsInput.js");

        private List<CodeRegion> Act() => Act(DefaultFilePath);
        private List<CodeRegion> ActOnMultipleDemoRegions() => Act(MultipleDemoRegionsPath);

        private List<CodeRegion> Act(string filePath)
        {
            var tokenizer = TokenizerFactory.GetFor(DemoLanguage.Java);
            var parser = new RegionParser(filePath, tokenizer);
            var result = parser.GetRegions();
            return result.ToList();
        }
    }
}
