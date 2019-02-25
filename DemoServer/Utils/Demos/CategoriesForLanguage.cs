using System.Collections.Generic;
using DemoParser.Models;

namespace DemoServer.Utils.Demos
{
    public class CategoriesForLanguage
    {
        public DemoLanguage Language { get; set; }

        public List<CategoryForLanguage> Categories { get; set; }
    }

    public class CategoryForLanguage
    {
        public string Slug { get; set; }

        public List<DemoForLanguage> Demos { get; set; }
    }

    public class DemoForLanguage
    {
        public string Slug { get; set; }
        public string Title { get; set; }
    }
}
