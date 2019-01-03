using System.IO;
using DemoParser.CodeParsing;

namespace DemoParser.Tests.DemoCodeBuilderTests
{
    public abstract class DemoCodeBuilderTests
    {
        protected DemoCodeBuilder GetBuilder(string filePath) => DemoCodeBuilder.Initialize(filePath);

        protected string GetExpectedOutput(string filePath) => File.ReadAllText(filePath);
    }
}
