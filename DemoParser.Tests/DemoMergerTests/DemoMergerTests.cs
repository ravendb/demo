using System;
using System.Collections.Generic;
using DemoParser.CodeParsing;
using DemoParser.Models;
using DemoParser.Utils;
using Xunit;

namespace DemoParser.Tests.DemoMergerTests
{
    public class DemoMergerTests
    {
        [Fact]
        public void CopiesWalkthroughs()
        {
            var src = DemoCodeFixtureBuilder.Initialize()
                .WithMutlipleWalkthrougs()
                .Build();

            var dest = DemoFixtureBuilder.Initialize()
                .WithMultipleWalkthroughs()
                .Build();

            var result = Act(src, dest);

            Assert.All(result.Walkthroughs, w =>
            {
                var range = w.Lines;
                Assert.True(range.Start > 0);
                Assert.True(range.End > 0);
            });
        }

        [Fact]
        public void ThrowsException_WhenNoSourceCodeProvided()
        {
            var src = DemoCodeFixtureBuilder.Initialize()
                .WithNullSourceCode()
                .WithMutlipleWalkthrougs()
                .Build();

            var dest = DemoFixtureBuilder.Initialize()
                .WithMultipleWalkthroughs()
                .Build();

            try
            {
                Act(src, dest);
                Assert.True(false, $"Should have thrown {nameof(ParsingException)}.");
            }
            catch (ParsingException)
            {
            }
        }

        [Fact]
        public void MatchesWalkthroughs_ByMetadataOrder()
        {
            var src = DemoCodeFixtureBuilder.Initialize()
                .WithUnorderedWalkthroughs()
                .Build();

            var dest = DemoFixtureBuilder.Initialize()
                .WithMultipleWalkthroughs()
                .Build();

            var result = Act(src, dest);

            AssertWalkthroughRangesAreInOrder(result.Walkthroughs);
        }

        [Fact]
        public void MatchesWalkthroughs_ByMetadataOrder_IgnoringCasing()
        {
            var src = DemoCodeFixtureBuilder.Initialize()
                .WithWalkthroughsNonStandardCasing()
                .Build();

            var dest = DemoFixtureBuilder.Initialize()
                .WithMultipleWalkthroughs()
                .Build();

            var result = Act(src, dest);

            AssertWalkthroughRangesAreInOrder(result.Walkthroughs);
        }

        [Fact]
        public void ThrowsException_WhenDuplicatedWalkthroughsInSource()
        {
            var src = DemoCodeFixtureBuilder.Initialize()
                .WithDuplicatedWalkthroughs()
                .Build();

            var dest = DemoFixtureBuilder.Initialize()
                .WithMultipleWalkthroughs()
                .Build();

            try
            {
                Act(src, dest);
                Assert.True(false, $"Should have thrown {nameof(InvalidOperationException)}.");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [Fact]
        public void ThrowsException_WhenRegionIsMissingFromSource()
        {
            var src = DemoCodeFixtureBuilder.Initialize()
                .WithMissingWalkthroughs()
                .Build();

            var dest = DemoFixtureBuilder.Initialize()
                .WithMultipleWalkthroughs()
                .Build();

            try
            {
                Act(src, dest);
                Assert.True(false, $"Should have thrown {nameof(ParsingException)}.");
            }
            catch (ParsingException)
            {
            }
        }

        private Demo Act(DemoCodeBuilder.Output src, Demo dest)
        {
            var demoMerger = new DemoMerger(src);
            demoMerger.MergeTo(dest);
            return dest;
        }

        private void AssertWalkthroughRangesAreInOrder(IReadOnlyList<DemoWalkthrough> walkthroughs)
        {
            for (var i = 1; i < walkthroughs.Count; i++)
            {
                var prevLines = walkthroughs[i - 1].Lines;
                var currentLines = walkthroughs[i].Lines;
                Assert.True(prevLines.Start < currentLines.Start);
                Assert.True(prevLines.End < currentLines.End);
            }
        }
    }
}
