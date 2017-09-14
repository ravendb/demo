using System.Collections.Generic;
using System.Linq;
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
        [Route("/basic/projectionQuery")]
        [Demo("Projection Query", DemoOutputType.Flatten, demoOrder: 80)]
        public object ProjectionQuery(string country = "USA")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var namesList = new List<object>();

                var query =
                    session.Query<NameAndCountry.Result, NameAndCountry>()
                        .Search(x => x.Country, country)
                        .Select(x => new NameAndCountry.Result
                        {
                            Country = x.Country,
                            Id = x.Id,
                            Name = x.Name
                        });

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