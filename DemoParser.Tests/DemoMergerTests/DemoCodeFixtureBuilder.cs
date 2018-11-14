using System.Collections.Generic;
using DemoParser.CodeParsing;
using DemoParser.Models;

namespace DemoParser.Tests.DemoMergerTests
{
    internal class DemoCodeFixtureBuilder
    {
        public const int DefaultUsingsLastLine = 5;

        private readonly DemoCodeBuilder.Output _result;

        private DemoCodeFixtureBuilder()
        {
            _result = new DemoCodeBuilder.Output
            {
                SourceCode = "public class Something { };",
                UsingsLastLine = DefaultUsingsLastLine
            };
        }

        public static DemoCodeFixtureBuilder Initialize()
        {
            return new DemoCodeFixtureBuilder();
        }

        public DemoCodeFixtureBuilder WithNullSourceCode()
        {
            _result.SourceCode = null;
            return this;
        }

        public DemoCodeFixtureBuilder WithMutlipleWalkthrougs()
        {
            _result.Walkthroughs = new List<DemoCodeBuilder.WalkthroughOutput>
            {
                Walk1,
                Walk2,
                Walk3
            };
            return this;
        }

        public DemoCodeFixtureBuilder WithUnorderedWalkthroughs()
        {
            _result.Walkthroughs = new List<DemoCodeBuilder.WalkthroughOutput>
            {
                Walk2,
                Walk3,
                Walk1
            };
            return this;
        }

        public DemoCodeFixtureBuilder WithDuplicatedWalkthroughs()
        {
            _result.Walkthroughs = new List<DemoCodeBuilder.WalkthroughOutput>
            {
                Walk1,
                Walk1,
                Walk2,
                Walk2,
                Walk2,
                Walk3
            };
            return this;
        }

        public DemoCodeFixtureBuilder WithWalkthroughsNonStandardCasing()
        {
            _result.Walkthroughs = new List<DemoCodeBuilder.WalkthroughOutput>
            {
                WalkWrongCasing1,
                WalkWrongCasing2,
                WalkWrongCasing3
            };
            return this;
        }

        public DemoCodeFixtureBuilder WithMissingWalkthroughs()
        {
            _result.Walkthroughs = new List<DemoCodeBuilder.WalkthroughOutput>
            {
                Walk1
            };
            return this;
        }

        public DemoCodeBuilder.Output Build()
        {
            return _result;
        }

        private DemoCodeBuilder.WalkthroughOutput Walk1 => new DemoCodeBuilder.WalkthroughOutput
        {
            Range = new LinesRange {Start = 3, End = 5},
            RegionName = "Step_1"
        };

        private DemoCodeBuilder.WalkthroughOutput Walk2 => new DemoCodeBuilder.WalkthroughOutput
        {
            Range = new LinesRange {Start = 7, End = 11},
            RegionName = "Step_2"
        };

        private DemoCodeBuilder.WalkthroughOutput Walk3 => new DemoCodeBuilder.WalkthroughOutput
        {
            Range = new LinesRange {Start = 15, End = 18},
            RegionName = "Step_3"
        };

        private DemoCodeBuilder.WalkthroughOutput WalkWrongCasing1 => BreakCasing(Walk1);
        private DemoCodeBuilder.WalkthroughOutput WalkWrongCasing2 => BreakCasing(Walk2);
        private DemoCodeBuilder.WalkthroughOutput WalkWrongCasing3 => BreakCasing(Walk3);

        private DemoCodeBuilder.WalkthroughOutput BreakCasing(DemoCodeBuilder.WalkthroughOutput walk)
        {
            walk.RegionName = walk.RegionName.ToUpper();
            return walk;
        }
    }
}
