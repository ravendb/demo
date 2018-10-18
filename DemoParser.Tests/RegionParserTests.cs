using System.Linq;
using DemoParser.Regions;
using Xunit;

namespace DemoParser.Tests
{
    public class RegionParserTests
    {
        [Fact]
        public void RetrievesRegions()
        {
            var parser = new RegionParser(DefaultFilePath);
            var result = parser.GetRegions().ToList();
        }

        private const string DefaultFilePath = "MockSrc\\CSharp\\Basics\\Demo101\\Demo101Controller.cs";
    }
}
