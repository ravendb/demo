using System;
using System.Collections.Generic;
using System.Linq;
using DemoParser.Models;
using Xunit;

namespace DemoParser.Tests
{
    public class ParserTests
    {
        [Fact]
        public void ParsesDemoCategories()
        {
            var result = Act(DefaultSettings);

            Assert.NotEmpty(result);

            var firstResult = result[0];
            Assert.False(string.IsNullOrEmpty(firstResult.Slug));
            Assert.False(string.IsNullOrEmpty(firstResult.Title));
            Assert.False(string.IsNullOrEmpty(firstResult.Directory));
        }

        [Fact]
        public void ParsesMultipleCategories()
        {
            var result = Act(DefaultSettings);
            Assert.True(result.Count > 1);
        }

        [Fact]
        public void ParsesMultipleDemosForCategory()
        {
            var result = Act(DefaultSettings);

            var firstResult = result[0];
            var demos = firstResult.Demos;

            Assert.NotEmpty(demos);
            Assert.True(demos.Count > 1);
        }

        [Fact]
        public void GetsDemoMetadata()
        {
            var result = Act(DefaultSettings);

            AssertAllDemos(result, Assert.NotNull);
            AssertAllDemos(result, d => Assert.NotEmpty(d.Title));
            AssertAllDemos(result, d => Assert.NotEmpty(d.DescriptionHtml));
            AssertAllDemos(result, d => Assert.NotEmpty(d.Assets));
            AssertAllDemos(result, d => Assert.NotEmpty(d.Walkthroughs));
        }

        [Fact]
        public void GetsDemoCode()
        {
            var result = Act(DefaultSettings);

            AssertAllDemos(result, d => Assert.NotEmpty(d.SourceCode));
            AssertAllDemos(result, d => Assert.True(d.UsingsLastLine > 0));

            AssertAllWalkthroughs(result, w => Assert.True(w.Lines.Start <= w.Lines.End));
            AssertAllWalkthroughs(result, w => Assert.NotEmpty(w.Title));
        }

        [Fact]
        public void SetsDemoHash()
        {
            var result = Act(DefaultSettings);
            AssertAllDemos(result, d => Assert.NotEmpty(d.Hash));
        }

        [Fact]
        public void SetsSlugs()
        {
            var result = Act(DefaultSettings);
            Assert.All(result, c => Assert.NotEmpty(c.Slug));
            AssertAllDemos(result, d => Assert.NotEmpty(d.Slug));
        }

        [Fact]
        public void SetsLowercaseSlugs()
        {
            var result = Act(DefaultSettings);
            Assert.All(result, c => AssertIsLowercase(c.Slug));
            AssertAllDemos(result, d => AssertIsLowercase(d.Slug));
        }

        [Fact]
        public void SetsOptionalFieldsToFalse_IfNotPresent()
        {
            var result = Act(DefaultSettings);
            var demo = GetDemo(result, "basics", "demo101");

            Assert.NotNull(demo);

            Assert.False(demo.NonInteractive);
            Assert.False(demo.StudioLinkToMediaDatabase);
            Assert.False(demo.StudioLinkToIndexList);
            Assert.False(demo.ConferenceOnly);
        }

        [Fact]
        public void SetsOptionalFieldsToTrue_IfPresent()
        {
            var result = Act(DefaultSettings);
            var demo = GetDemo(result, "indexing", "demo201");

            Assert.NotNull(demo);

            Assert.True(demo.NonInteractive);
            Assert.True(demo.StudioLinkToMediaDatabase);
            Assert.True(demo.StudioLinkToIndexList);
            Assert.True(demo.ConferenceOnly);
        }

        private ParserSettings DefaultSettings => new ParserSettings
        {
            SourceCodeFolder = "MockSrc\\CSharp",
            Language = DemoLanguage.CSharp
        };

        private List<DemoCategory> Act(ParserSettings settings)
        {
            var parser = new Parser(settings);
            var result = parser.Run().ToList();
            return result;
        }

        private Demo GetDemo(IEnumerable<DemoCategory> results, string categorySlug, string demoSlug)
        {
            var category = results.Single(x => string.Equals(x.Slug, categorySlug, StringComparison.OrdinalIgnoreCase));
            var demo = category.Demos.Single(x => string.Equals(x.Slug, demoSlug, StringComparison.OrdinalIgnoreCase));

            return demo;
        }

        private void AssertAllDemos(List<DemoCategory> result, Action<Demo> demoAssertion)
        {
            Assert.All(result, category => Assert.All(category.Demos, demoAssertion));
        }

        private void AssertAllWalkthroughs(List<DemoCategory> result, Action<DemoWalkthrough> walkthroughAssertion)
        {
            Assert.All(result, category => Assert.All(category.Demos, demo => Assert.All(demo.Walkthroughs, walkthroughAssertion)));
        }

        private void AssertIsLowercase(string text)
        {
            Assert.NotNull(text);
            Assert.Equal(text.ToLower(), text);
        }
    }
}
