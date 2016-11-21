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
        [Route("/basic/query1")]
        [Demo("Query I", DemoOutputType.Flatten, demoOrder: 40)]
        public object Query1(string country = "UK")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                RavenQueryStatistics stats;

                var query = 
                    from company in session.Query<Company>().Statistics(out stats)
                    where company.Address.Country == country
                    select company;

                var result = query.FirstOrDefault();

                ServerTime = TimeSpan.FromMilliseconds(stats.DurationMilliseconds);

                return result;
            }
        }
    }
}

