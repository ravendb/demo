using System;

namespace DemoCommon.Utils
{
    public static class DatabaseName
    {
        public const string UserDatabasePrefix = "User-";

        public static string For(Guid userId) => $"{UserDatabasePrefix}{userId.ToString()}";
    }
}
