using System.Collections.Generic;
using DemoParser.Models;

namespace DemoParser.Utils
{
    internal static class LanguageMaps
    {
        private static readonly Dictionary<DemoLanguage, string> Folders = new Dictionary<DemoLanguage, string>
        {
            {DemoLanguage.CSharp, "CSharp"},
            {DemoLanguage.Java, "Java" },
            {DemoLanguage.Python, "Python" }
        };

        public static string GetFolderFor(DemoLanguage language) => Folders[language];

        private static readonly Dictionary<DemoLanguage, string> CodeFiles = new Dictionary<DemoLanguage, string>
        {
            {DemoLanguage.CSharp, "Code.cs"},
            {DemoLanguage.Java, "Code.java"},
            {DemoLanguage.Python, "Code.py"}
        };

        public static string GetFileNameFor(DemoLanguage language) => CodeFiles[language];
    }
}
