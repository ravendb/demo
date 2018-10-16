using System.Collections.Generic;
using DemoParser.Models;

namespace DemoParser.Utils
{
    internal static class FolderMap
    {
        private static readonly Dictionary<DemoLanguage, string> _map = new Dictionary<DemoLanguage, string>
        {
            {DemoLanguage.CSharp, "CSharp"},
            {DemoLanguage.Java, "Java" },
            {DemoLanguage.Python, "Python" }
        };

        public static string GetFor(DemoLanguage language) => _map[language];
    }
}
