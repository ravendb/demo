using System;
using System.Collections.Generic;

namespace DemoMethods.Entities
{
    public class LastFm
    {
        public string Artist { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<List<string>> Similars { get; set; }
        public List<List<string>> Tags { get; set; }
        public string Track_Id { get; set; }
        public string Title { get; set; }
    }
}
