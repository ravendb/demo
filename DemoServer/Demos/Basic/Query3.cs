using System;
using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Client;
using Raven.Client.Linq;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        [HttpGet]
        [Route("/basic/query3")]
        [Demo("Query III", DemoOutputType.Flatten, demoOrder: 60)]
        public object Query3(string city = "Berlin")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                RavenQueryStatistics stats;
                var query =
                    from company in session.Query<Company>().Statistics(out stats)
                    where company.Address.City == city
                    select company;

                var result = query.FirstOrDefault();

                ServerTime = TimeSpan.FromMilliseconds(stats.DurationMilliseconds);

                return result;
            }
        }
    }
}