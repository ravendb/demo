using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using Raven.Client;
using Raven.Client.Linq;

namespace DemoMethods.Basic
{
    public partial class BasicController : DemoApiController
    {

        [HttpGet]
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

