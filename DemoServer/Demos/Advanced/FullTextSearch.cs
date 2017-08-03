using System;
using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/fullTextSearch")]
        [Demo("Full Text Search", DemoOutputType.Flatten, demoOrder: 200)]
        public object FullTextSearch(string searchTerm = "Jazz")
        {
            using (var session = DocumentStoreHolder.MediaStore.OpenSession())
            {
                QueryStatistics stats;
                var results = session.Query<LastFmAnalyzed.Result, LastFmAnalyzed>()
                    .Take(20)
                    .Search(x => x.Query, searchTerm)
                    .TransformWith<TransformerLastFm, LastFm>()
                    .Statistics(out stats)
                    .ToList();

                ServerTime = TimeSpan.FromMilliseconds(stats.DurationInMs);

                return results;
            }
        }
    }
}
