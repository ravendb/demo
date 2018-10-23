using System.Collections.Generic;
using DemoParser.Models;

namespace DemoParser.Utils.Json
{
    internal class JsonDemoSet
    {
        public JsonCategory Category { get; set; }

        public List<string> Demos { get; set; }
    }

    internal class JsonCategory
    {
        public string Name { get; set; }

        public string Folder { get; set; }

        public string Description { get; set; }
    }

    internal class JsonMetadata : Demo
    {
        public string SourceFileName { get; set; }
    }
}
