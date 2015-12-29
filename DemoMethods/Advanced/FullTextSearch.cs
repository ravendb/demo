using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        [Demo("Full Text Search", DemoOutputType.Flatten)]
        public object FullTextSearch(string searchTerm = "Jazz")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Query<LastFmAnalyzed.Result, LastFmAnalyzed>()
                    .Search(x => x.Query, searchTerm)
                    .TransformWith<TransformerLastFm, LastFm>()
                    .ToList();
            }
        }
    }
}
