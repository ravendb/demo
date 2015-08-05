using DemoMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentStoreHolder.SetDbInfo("localhost", 8080, "Northwind");

            var server = new DemoServer();
            server.Start("localhost", 9090);
        }
    }
}
