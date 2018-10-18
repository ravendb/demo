using System.IO;
using System.Text;

namespace DemoParser.Regions
{
    public class CodeSlicer
    {
        public string GetSliceFor(string filePath, CodeRegion region)
        {
            var lines = File.ReadAllLines(filePath);
            var resultBuilder = new StringBuilder();

            for (var i = region.LineStart; i < region.LineEnd - 1; i++)
                resultBuilder.AppendLine(lines[i]);

            return resultBuilder.ToString();
        }
    }
}
