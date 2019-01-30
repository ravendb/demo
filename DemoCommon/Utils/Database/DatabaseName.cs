using System;
using DemoCommon.Models;

namespace DemoCommon.Utils.Database
{
    public class DatabaseName
    {
        private readonly DatabaseSettings _databaseSettings;
        private readonly bool _conferenceMode;

        public DatabaseName(DatabaseSettings databaseSettings, bool conferenceMode)
        {
            _databaseSettings = databaseSettings;
            _conferenceMode = conferenceMode;
        }

        public const string UserDatabasePrefix = "DemoUser";
        public const string MediaDatabasePrefix = "Media";

        public string For(Guid userId) => _conferenceMode
            ? _databaseSettings.Name
            : $"{UserDatabasePrefix}-{userId.ToString()}";

        public string MediaFor(Guid userId) => _conferenceMode
            ? MediaDatabasePrefix
            : $"{MediaDatabasePrefix}-{userId.ToString()}";
    }
}
