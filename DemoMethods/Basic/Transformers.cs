using DemoMethods.Indexes;
using Raven.Client;
using Raven.Client.Indexes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {        
        public class Transformer_NameAndCountry : AbstractTransformerCreationTask<Index_NameAndCountry.Result>
        {
            public Transformer_NameAndCountry()
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
            Store.ExecuteIndex(new Index_NameAndCountry());
            Store.ExecuteTransformer(new Transformer_NameAndCountry());

            // TODO :: here and basiclly in all other places : staleness indexes .. need to verify

            using (var session = Store.OpenSession())
            {

                var namesList = new List<string>();

                var query =
                    session.Query<Index_NameAndCountry.Result, Index_NameAndCountry>()
                    .TransformWith<Transformer_NameAndCountry, Index_NameAndCountry.Result>() //TypeOf - client side projection (serialization takes place in client side)
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