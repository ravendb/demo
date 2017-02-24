using System;
using System.Collections.Generic;
using System.Linq;
using DemoServer.Controllers;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        [HttpGet]
        [Route("/basic/mapReduceUsingDocumentQuery")]
        [Demo("MapReduce Using Document Query", DemoOutputType.Flatten, demoOrder: 110)]
        public object MapReduceUsingDocumentQuery()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                QueryStatistics stats;
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