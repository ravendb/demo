using System;
using System.Collections.Generic;

namespace DemoServer.Entities
{
    public class LastFm
    {
        public string Artist { get; set; }
        public string TrackId { get; set; }
        public string Title { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<string> Tags { get; set; }
    }
}
