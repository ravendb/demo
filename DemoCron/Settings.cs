using DemoCommon.Models;

namespace DemoCron
{
    public class Settings
    {
        public DatabaseSettings Database { get; set; }
        public DeleteUnusedDatabasesSettings DeleteUnusedDatabases { get; set; }

        public class DeleteUnusedDatabasesSettings
        {
            public TimeSpanSettings ExpirationSpan { get; set; }
        }

        public class TimeSpanSettings
        {
            public int? Days { get; set; }
            public int? Hours { get; set; }
            public int? Minutes { get; set; }
            public int? Seconds { get; set; }
        }
    }
}
