using System.Collections.Generic;
using DemoParser.Models;

namespace DemoParser.Tests.DemoMergerTests
{
    internal class DemoFixtureBuilder
    {
        private readonly Demo _result;

        private DemoFixtureBuilder()
        {
            _result = new Demo
            {
                Slug = "demo-slug"
            };
        }

        public static DemoFixtureBuilder Initialize()
        {
            return new DemoFixtureBuilder();
        }

        public DemoFixtureBuilder WithMultipleWalkthroughs()
        {
            _result.Walkthroughs = new List<DemoWalkthrough>
            {
                _walk1,
                _walk2,
                _walk3
            };
            return this;
        }

        public Demo Build()
        {
            return _result;
        }

        private readonly DemoWalkthrough _walk1 = new DemoWalkthrough
        {
            Assets = new List<DemoAsset>
            {
                new DemoAsset
                {
                    Title = "WT AssetTitle1",
                    Type = AssetType.Document,
                    Url = "someurl"
                },
                new DemoAsset
                {
                    Title = "WT AssetTitle2",
                    Type = AssetType.Document,
                    Url = "someurl2"
                },
            },
            Title = "walk_1",
            DescriptionHtml = "some description"
        };

        private readonly DemoWalkthrough _walk2 = new DemoWalkthrough
        {
            Assets = new List<DemoAsset>
            {
                new DemoAsset
                {
                    Title = "WT AssetTitle3",
                    Type = AssetType.Document,
                    Url = "someurl"
                }
            },
            Title = "walk_2",
            DescriptionHtml = "some description 2"
        };

        private readonly DemoWalkthrough _walk3 = new DemoWalkthrough
        {
            Assets = new List<DemoAsset>
            {
                new DemoAsset
                {
                    Title = "WT AssetTitle4",
                    Type = AssetType.Document,
                    Url = "someurl"
                }
            },
            Title = "walk_3",
            DescriptionHtml = "some description 3"
        };
    }
}
