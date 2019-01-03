using System;
using DemoCommon.Models;
using DemoCommon.Utils.Database;

namespace DemoServer.Utils.Database
{
    public class DatabaseLinks
    {
        private readonly DatabaseSettings _databaseSettings;
        private readonly DatabaseName _databaseName;

        public DatabaseLinks(Settings settings, DatabaseName databaseName)
        {
            _databaseName = databaseName;
            _databaseSettings = settings.Database;
        }

        private string FirstDatabaseUrl => _databaseSettings.Urls[0];

        private string StudioUrl => $"{FirstDatabaseUrl}/studio/index.html";

        public string ToDocuments(Guid userId)
        {
            var databaseName = _databaseName.For(userId);
            return $"{StudioUrl}#databases/documents?&database={databaseName}";
        }
    }
}
