using System;
using System.Collections.Generic;
using System.Linq;
using DemoParser.Regions;
using DemoParser.Utils;

namespace DemoParser.CodeParsing
{
    internal class RegionContainer
    {
        private readonly List<CodeRegion> _regions;

        public RegionContainer(List<CodeRegion> regions)
        {
            _regions = regions;

            Usings = GetRegionByName(RegionNames.Usings);
            Demo = GetRegionByName(RegionNames.Demo);
            Walk = GetRegionsByPrefix(RegionNames.WalkthroughPrefix);
        }

        public CodeRegion Usings { get; }
        public CodeRegion Demo { get; }
        public List<CodeRegion> Walk { get; }

        private CodeRegion GetRegionByName(string name)
        {
            return _regions.SingleOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        private List<CodeRegion> GetRegionsByPrefix(string prefix)
        {
            return _regions
                .Where(x => x.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public CodeRegion GetWalkRegionStartingWithLine(int lineNum)
        {
            return Walk.SingleOrDefault(x => x.LineStart == lineNum);
        }

        public CodeRegion GetWalkRegionEndingWithLine(int lineNum)
        {
            return Walk.SingleOrDefault(x => x.LineEnd == lineNum);
        }
    }
}
