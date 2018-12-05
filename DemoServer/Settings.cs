namespace DemoServer
{
    public class Settings
    {
        public DatabaseSettings Database { get; set; }

        public class DatabaseSettings
        {
            public string[] Urls { get; set; }
            public string Name { get; set; }
            public string CertificatePath { get; set; }
            public string CertificatePassword { get; set; }
            public int? RequestsTimeoutInSec { get; set; }
        }
    }
}
