using System.Linq;
using DemoServer.Controllers;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;

namespace DemoServer.Demos.Basic
{
    public partial class BasicController : BaseController
    {
        [HttpGet]
        [Route("/basic/multiMapIndexingQuery")]
        [Demo("MultiMap Indexing Query", DemoOutputType.Flatten, demoOrder: 100)]
        public object MultiMapIndexingQuery(string country = "USA")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Query<NameAndCountry.Result, NameAndCountry>()
                    .Search(x => x.Country, country)
                    .Select(x => new { x.Name, x.Id })
                    .ToList();
            }
        }
    }
}