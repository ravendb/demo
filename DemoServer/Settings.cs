using DemoCommon.Models;

namespace DemoServer
{
    public class Settings
    {
        public DatabaseSettings Database { get; set; }

        public bool ConferenceMode { get; set; }

        public GoogleTagManagerSettings GoogleTagManager { get; set; }

        public class GoogleTagManagerSettings
        {
            public string ContainerId { get; set; }
        }
    }
}
