using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DemoParser.Utils.Json
{
    internal class JsonImporter
    {
        private TOutput Get<TOutput>(string folderPath, string fileName)
        {
            var filePath = Path.Join(folderPath, fileName);

            if (File.Exists(filePath) == false)
                throw new ParsingException($"File {filePath} is missing.");

            using (var streamReader = new StreamReader(filePath))
            {
                var text = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<TOutput>(text);
            }
        }

        public List<JsonDemoSet> GetDemoSets(string folderPath) => Get<List<JsonDemoSet>>(folderPath, FileNames.Demos);

        public JsonMetadata GetMetadata(string folderPath) => Get<JsonMetadata>(folderPath, FileNames.Metadata);
    }
}
