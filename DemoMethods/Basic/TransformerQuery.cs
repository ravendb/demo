using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {                
        [HttpGet]
        public object TransformerQuery()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                var country = nvc["Country"] ?? "USA";

                var namesList = new List<string>();

                var query =
                    session.Query<IndexNameAndCountry.Result, IndexNameAndCountry>()
                    .TransformWith<TransformerNameAndCountry, IndexNameAndCountry.Result>() 
                    .Search(x => x.Country, country);
                // TODO: with to list, add another one for streaming

                using (var enumerator = session.Advanced.Stream(query))
                {
                    while (enumerator.MoveNext())
                    {
                        var result = enumerator.Current.Document;
                        namesList.Add(result.Name);
                    }
                }
                return namesList;
            }
        }
    }
}