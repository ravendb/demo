using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DemoMethods.Indexes;
using Raven.Client;
using Raven.Client.Indexes;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {        
        public class TransformerNameAndCountry : AbstractTransformerCreationTask<IndexNameAndCountry.Result>
        {
            public TransformerNameAndCountry()
            {
                TransformResults = results => from result in results
                                              select new
                                              {
                                                  result.Name
                                              };
            }
        }

        [HttpGet]
        public object TransformerQuery()
        {
            Store.ExecuteIndex(new IndexNameAndCountry());
            Store.ExecuteTransformer(new TransformerNameAndCountry());

            // TODO :: here and basiclly in all other places : staleness indexes .. need to verify

            using (var session = Store.OpenSession())
            {

                var namesList = new List<string>();

                var query =
                    session.Query<IndexNameAndCountry.Result, IndexNameAndCountry>()
                    .TransformWith<TransformerNameAndCountry, IndexNameAndCountry.Result>() //TypeOf - client side projection (serialization takes place in client side)
                    .Search(x => x.Country, "USA");

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