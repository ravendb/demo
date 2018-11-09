using DemoServer.Utils;

namespace DemoServer.Models
{
    public class DemoVersionDto
    {
        public string Category { get; set; }
        public string Demo { get; set; }
        public string Hash { get; set; }

        public static DemoVersionDto FromEntry(DemoContainer.DemoVersionEntry entry)
        {
            return new DemoVersionDto
            {
                Category = entry.Category,
                Demo = entry.Demo,
                Hash = entry.Hash
            };
        }
    }
}
