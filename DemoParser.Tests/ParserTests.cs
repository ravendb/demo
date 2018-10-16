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
            Assert.False(string.IsNullOrEmpty(firstResult.Title));
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

            var firstResult = result[0];
            var firstDemo = firstResult.Demos[0];

            Assert.NotNull(firstDemo);
            Assert.NotEmpty(firstDemo.Assets);
            Assert.NotEmpty(firstDemo.Params);
            Assert.NotEmpty(firstDemo.Walkthroughs);
        }

        private ParserSettings DefaultSettings => new ParserSettings
        {
            RootSourceFolder = "MockSrc",
            Language = DemoLanguage.CSharp
        };

        private List<DemoCategory> Act(ParserSettings settings)
        {
            var parser = new Parser(settings);
            var result = parser.Run().ToList();
            return result;
        }
    }
}
