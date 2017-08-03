using System.Collections.Generic;
using DemoServer.Controllers;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        public class Result
        {
            public string Name;
        }

        [HttpGet]
        [Route("/basic/transformerQuery")]
        [Demo("Transformer Query", DemoOutputType.Flatten, demoOrder: 80)]
        public object TransformerQuery(string country = "USA")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var namesList = new List<object>();

                var query =
                    session.Query<NameAndCountry.Result, NameAndCountry>()
                        .TransformWith<TransformerNameAndCountry, NameAndCountry.Result>()
                        .Search(x => x.Country, country);

                RecordQuery(query);

                // Stream the results:
                using (var enumerator = session.Advanced.Stream(query))
                {
                    while (enumerator.MoveNext())
                    {
                        var result = enumerator.Current.Document;
                        namesList.Add(new { result.Name });
                    }
                }

                return namesList;
            }
        }
    }
}