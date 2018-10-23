namespace DemoServer
{
    public class Settings
    {
        public ParserSetting Parser { get; set; }

        public class ParserSetting
        {
            public string SourceFolder { get; set; }
        }
    }
}
