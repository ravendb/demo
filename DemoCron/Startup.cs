namespace DemoCron
{
    public class Startup
    {
        public Startup(Settings settings)
        {
            Settings = settings;
        }

        public Settings Settings { get; }

        public void Configure()
        {
        }
    }
}
