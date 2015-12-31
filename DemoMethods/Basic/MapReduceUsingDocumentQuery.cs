using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DemoMethods.Helpers;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : DemoApiController
    {
        [HttpGet]
        [Demo("MapReduce Using Document Query", DemoOutputType.Flatten, demoOrder: 110)]
        public object MapReduceUsingDocumentQuery()
        {
            RavenQueryStatistics stats;

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                IList<ProductSales.Result> results = session
                    .Advanced
                    .DocumentQuery<ProductSales.Result, ProductSales>().Statistics(out stats)
                    .ToList();

                ServerTime = TimeSpan.FromMilliseconds(stats.DurationMilliseconds);

                return results;
            }
        }
    }
}