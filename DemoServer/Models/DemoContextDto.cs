using System.Collections.Generic;
using DemoServer.Utils.Demos;

namespace DemoServer.Models
{
    public class DemoContextDto
    {
        public List<CategoriesForLanguage> CategoriesForLanguages { get; set; }

        public List<CategoryWithDemoVersions> CategoriesWithVersions { get; set; }

        public bool ConferenceMode { get; set; }
    }
}
