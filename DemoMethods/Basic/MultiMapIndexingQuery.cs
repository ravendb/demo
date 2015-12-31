using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Helpers;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : DemoApiController
    {
        [HttpGet]
        public object MultiMapIndexingQuery(string country = "USA")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Query<NameAndCountry.Result, NameAndCountry>()
                    .Customize(x => x.WaitForNonStaleResults())
                    .Search(x => x.Country, country)
                    .Select(x => new {x.Name, x.Id})
                    .ToList();
            }
        }
    }
}