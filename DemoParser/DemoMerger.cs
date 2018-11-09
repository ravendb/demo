using System;
using System.Linq;
using DemoParser.CodeParsing;
using DemoParser.Models;
using DemoParser.Utils;

namespace DemoParser
{
    public class DemoMerger
    {
        private readonly DemoCodeBuilder.Output _codeOutput;

        public DemoMerger(DemoCodeBuilder.Output codeOutput)
        {
            _codeOutput = codeOutput;
        }

        public void MergeTo(Demo destDemo)
        {
            ValidateSourceCode(destDemo);

            destDemo.SourceCode = _codeOutput.SourceCode;
            destDemo.UsingsLastLine = _codeOutput.UsingsLastLine;
            destDemo.Hash = _codeOutput.FileHash;

            var destWalkthroughs = destDemo.Walkthroughs;

            for (var i = 0; i < destWalkthroughs.Count; i++)
            {
                var destWalkthrough = destWalkthroughs[i];
                var srcWalkthrough = GetSourceWalkthroughByMetadataCount(i);

                destWalkthrough.Lines = srcWalkthrough.Range;
            }
        }

        private void ValidateSourceCode(Demo destDemo)
        {
            if (string.IsNullOrEmpty(_codeOutput.SourceCode))
                throw new ParsingException($"Source code for demo {destDemo.Slug} was null or empty");
        }

        private DemoCodeBuilder.WalkthroughOutput GetSourceWalkthroughByMetadataCount(int count)
        {
            var regionName = $"{RegionNames.WalkthroughPrefix}{count + 1}";

            var srcWalkthrough = _codeOutput.Walkthroughs
                .SingleOrDefault(x => string.Equals(x.RegionName, regionName, StringComparison.OrdinalIgnoreCase));

            if (srcWalkthrough == null)
                throw new ParsingException($"Could not find region {regionName} in the source regions.");

            return srcWalkthrough;
        }
    }
}
