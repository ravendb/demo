using DemoMethods;

namespace DemoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentStoreHolder.SetDbInfo("localhost.fiddler", 8080, "Northwind");

            var server = new DemoServer();
            server.Start("localhost.fiddler", 9090);
        }
    }
}
