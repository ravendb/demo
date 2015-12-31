using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using Raven.Client;
using Raven.Client.Linq;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {

        [HttpGet]
        [Demo("Query II")]
        public object Query2(string eid = "ALFKI")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                RavenQueryStatistics stats;
                var q = 
                    from company in session.Query<Company>().Statistics(out stats)
                    where company.ExternalId == eid
                    select company;

                var result = q.FirstOrDefault();
                return new
                {
                    stats.IndexName,
                    Result = result
                };
            }
        }
    }
}

