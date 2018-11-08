using System.Collections.Generic;

namespace DemoServer.Models
{
    public class UserProgressDto
    {
        public UserProgressDto()
        {
            CompletedDemos = new List<DemoProgressDto>();
        }

        public List<DemoProgressDto> CompletedDemos { get; set; }
    }

    public class DemoProgressDto
    {
        public string CategorySlug { get; set; }
        public string DemoSlug { get; set; }
    }
}
