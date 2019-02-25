using System.Collections.Generic;

namespace DemoServer.Utils.Demos
{
    public class CategoryWithDemoVersions
    {
        public string Slug { get; set; }
        public string Title { get; set; }

        public List<DemoWithVersion> Demos { get; set; }
    }

    public class DemoWithVersion
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Hash { get; set; }
    }
}
