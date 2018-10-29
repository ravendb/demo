using System.Collections.Generic;

namespace DemoParser.Models
{
    public class DemoCategory
    {
        public DemoCategory()
        {
            Demos = new List<Demo>();
        }

        public string Slug { get; set; }

        public string Directory { get; set; }

        public List<Demo> Demos { get; set; }
    }
}
