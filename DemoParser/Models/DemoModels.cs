﻿using System.Collections.Generic;

namespace DemoParser.Models
{
    public class Demo
    {
        public Demo()
        {
            Assets = new List<DemoAsset>();
            Params = new List<DemoParameter>();
            Walkthroughs = new List<DemoWalkthrough>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SourceCode { get; set; }

        public int UsingsLastLine { get; set; }

        public List<DemoAsset> Assets { get; set; }

        public List<DemoParameter> Params { get; set; }

        public List<DemoWalkthrough> Walkthroughs { get; set; }
    }

    public class DemoAsset
    {
        public AssetType Type { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }

    public enum AssetType
    {
        Link,
        Downloadable,
        Document
    }

    public class DemoWalkthrough
    {
        public DemoWalkthrough()
        {
            Assets = new List<DemoAsset>();
        }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string DescriptionHtml { get; set; }

        public LinesRange Lines { get; set; }

        public List<DemoAsset> Assets { get; set; }
    }

    public class LinesRange
    {
        public int Start { get; set; }

        public int End { get; set; }
    }

    public class DemoParameter
    {
        public string Name { get; set; }

        public ParameterType Type { get; set; }

        public string Placeholder { get; set; }
    }

    public enum ParameterType
    {
        String,
        DateTime,
        Int,
        Double
    }
}