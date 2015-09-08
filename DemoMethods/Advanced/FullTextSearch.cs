using System.Collections.Specialized;
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
        public object FullTextSearch()
        {
            var userParams = new NameValueCollection
                {
                    {"Search", "+Jazz +Love"},
                };
            DemoUtilities.GetUserParameters(Request.RequestUri.Query, userParams);
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    var query = session.Query<IndexFullTextSearch.Result,IndexFullTextSearch>()
                               .Search(x => x.Query, userParams["Search"])
                               .TransformWith<TransformerLastFm, LastFm>()
                               .ToList();

                    var query2 = session.Advanced.DocumentQuery<LastFm>(new IndexFullTextSearch().IndexName)
                        .Search("Query", userParams["Search"])
                        .ToList();
                            

                    return query;
                }
                catch (System.Exception e)
                {
                    
                    throw;
                }
            }
        }
    }
}
