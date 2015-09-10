using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public object FullTextSearch(string searchTerm = "Jazz")
        {
            try
            {
                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    return session.Query<LastFmAnalyzed.Result, LastFmAnalyzed>()
                        .Search(x => x.Query, searchTerm)
                        .TransformWith<TransformerLastFm, LastFm>()
                        .ToList();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
