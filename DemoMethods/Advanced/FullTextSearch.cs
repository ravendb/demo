using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : DemoApiController
    {
        [HttpGet]
        [Demo("Full Text Search", DemoOutputType.Flatten, demoOrder: 200)]
        public object FullTextSearch(string searchTerm = "Jazz")
        {
            using (var session = DocumentStoreHolder.MediaStore.OpenSession())
            {
                RavenQueryStatistics stats;
                var results = session.Query<LastFmAnalyzed.Result, LastFmAnalyzed>()
                    .Take(20)
                    .Search(x => x.Query, searchTerm)
                    .TransformWith<TransformerLastFm, LastFm>()
                    .Statistics(out stats)
                    .ToList();

                ServerTime = TimeSpan.FromMilliseconds(stats.DurationMilliseconds);

                return results;
            }
        }
    }
}
