using System.Collections.Generic;

namespace DemoServer.Entities
{
    public class Region
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<Territory> Territories { get; set; }
    }
}
