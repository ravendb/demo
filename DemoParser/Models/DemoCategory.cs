using System.Collections.Generic;

namespace DemoParser.Models
{
    public class DemoCategory
    {
        public DemoCategory()
        {
            Demos = new List<Demo>();
        }

        public string Title { get; set; }

        public string FolderName { get; set; }

        public List<Demo> Demos { get; set; }
    }
}
