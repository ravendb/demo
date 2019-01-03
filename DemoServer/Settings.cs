using DemoCommon.Models;

namespace DemoServer
{
    public class Settings
    {
        public DatabaseSettings Database { get; set; }

        public bool ConferenceMode { get; set; }
    }
}
