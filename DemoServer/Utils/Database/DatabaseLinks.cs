using System;
using DemoCommon.Models;
using DemoCommon.Utils;
using DemoCommon.Utils.Database;

namespace DemoServer.Utils.Database
{
    public class DatabaseLinks
    {
        private readonly DatabaseSettings _databaseSettings;

        public DatabaseLinks(Settings settings)
        {
            _databaseSettings = settings.Database;
        }

        private string FirstDatabaseUrl => _databaseSettings.Urls[0];

        private string StudioUrl => $"{FirstDatabaseUrl}/studio/index.html";

        public string ToDocuments(Guid userId)
        {
            var databaseName = DatabaseName.For(userId);
            return $"{StudioUrl}#databases/documents?&database={databaseName}";
        }
    }
}
