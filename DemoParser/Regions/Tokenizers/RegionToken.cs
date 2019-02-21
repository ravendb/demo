namespace DemoParser.Regions.Tokenizers
{
    public class RegionToken
    {
        public string Name { get; set; }

        public int LineNumber { get; set; }

        public TokenType Type { get; set; }

        public int Depth { get; set; }
    }

    public enum TokenType
    {
        Start,
        End
    }
}
