using System;

namespace DemoServer.Models
{
    public class DemoStats
    {
        public const string DocumentId = "DemoStats/1";

        public string Id => DocumentId;

        public DateTime LastAccessed { get; private set; }

        public DemoStats()
        {
            LastAccessed = DateTime.UtcNow;
        }

        public void UpdateLastAccess() => LastAccessed = DateTime.UtcNow;
    }
}
