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
        public string Slug { get; set; }

        public string Directory { get; set; }

        public string Title { get; set; }
    }

    internal class JsonMetadata
    {
        public string SourceFileName { get; set; }

        public JsonMetadata()
        {
            Assets = new List<JsonDemoAsset>();
            Walkthroughs = new List<JsonDemoWalkthrough>();
        }

        public string Slug { get; set; }

        public string SourceCode { get; set; }

        public string Hash { get; set; }

        public int UsingsLastLine { get; set; }

        public string Title { get; set; }

        public List<string> DescriptionLines { get; set; }

        public bool NonInteractive { get; set; }

        public bool StudioLinkToMediaDatabase { get; set; }

        public bool StudioLinkToIndexList { get; set; }

        public bool ConferenceOnly { get; set; }

        public List<JsonDemoAsset> Assets { get; set; }

        public List<JsonDemoWalkthrough> Walkthroughs { get; set; }
    }

    public class JsonDemoAsset
    {
        public AssetType Type { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }

    public class JsonDemoWalkthrough
    {
        public JsonDemoWalkthrough()
        {
            Assets = new List<JsonDemoAsset>();
        }

        public string Title { get; set; }

        public string Slug { get; set; }

        public List<string> DescriptionLines { get; set; }

        public LinesRange Lines { get; set; }

        public List<JsonDemoAsset> Assets { get; set; }

        public JsonDemoLink DemoLink { get; set; }
    }

    public class JsonDemoLink
    {
        public string Url { get; set; }

        public string Title { get; set; }
    }
}
