using System;

namespace DemoServer.Utils.Database
{
    public static class DatabaseName
    {
        private const string UserDatabasePrefix = "User-";

        public static string For(Guid userId) => $"{UserDatabasePrefix}{userId.ToString()}";
    }
}
