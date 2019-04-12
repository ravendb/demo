using System.Collections.Generic;
using DemoServer.Utils.Demos;

namespace DemoServer.Models
{
    public class DemoContextDto
    {
        public List<CategoriesForLanguageDto> CategoriesForLanguages { get; set; }

        public List<CategoryWithDemoVersions> CategoriesWithVersions { get; set; }

        public bool ConferenceMode { get; set; }

        public string GoogleTagManagerContainerId { get; set; }
    }

    public class CategoriesForLanguageDto
    {
        public string Language { get; set; }

        public List<CategoryForLanguage> Categories { get; set; }

        public static CategoriesForLanguageDto FromModel(CategoriesForLanguage model)
        {
            return new CategoriesForLanguageDto
            {
                Categories = model.Categories,
                Language = model.Language.ToString().ToLower()
            };
        }
    }
}
