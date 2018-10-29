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

        private void AssertAllDemos(List<DemoCategory> result, Action<Demo> demoAssertion)
        {
            Assert.All(result, category => Assert.All(category.Demos, demoAssertion));
        }

        private void AssertAllWalkthroughs(List<DemoCategory> result, Action<DemoWalkthrough> walkthroughAssertion)
        {
            Assert.All(result, category => Assert.All(category.Demos, demo => Assert.All(demo.Walkthroughs, walkthroughAssertion)));
        }
    }
}
