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

