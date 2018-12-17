using System;

namespace DemoCommon.Models
{
    public class DemoStats
    {
        public const string DocumentId = "DemoStats/1";

        public string Id => DocumentId;

        public DateTime LastAccessedUtc { get; private set; }

        public DemoStats()
        {
            LastAccessedUtc = DateTime.UtcNow;
        }

        public void UpdateLastAccess() => LastAccessedUtc = DateTime.UtcNow;
    }
}
