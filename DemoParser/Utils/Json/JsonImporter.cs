using System.Collections.Generic;
using System.IO;
using DemoParser.Models;
using Newtonsoft.Json;

namespace DemoParser.Utils.Json
{
    internal class JsonImporter
    {
        private TOutput Get<TOutput>(string folderPath, string fileName)
        {
            var filePath = Path.Join(folderPath, fileName);

            using (var streamReader = new StreamReader(filePath))
            {
                var text = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<TOutput>(text);
            }
        }

        public List<JsonCategory> GetCategories(string folderPath) => Get<List<JsonCategory>>(folderPath, FileNames.Categories);

        public List<string> GetDemos(string folderPath) => Get<List<string>>(folderPath, FileNames.Demos);

        public Demo GetMetadata(string folderPath) => Get<Demo>(folderPath, FileNames.Metadata);
    }
}
