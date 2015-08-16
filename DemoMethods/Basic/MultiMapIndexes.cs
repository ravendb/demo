using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Indexes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object MultiMapIndexingQuery()
        {
            Store.ExecuteIndex(new Index_NameAndCountry());

            using (var session = Store.OpenSession())
            {

                return session.Query<Index_NameAndCountry.Result, Index_NameAndCountry>()
                    .Customize(x => x.WaitForNonStaleResults())
                    .Search(x => x.Country, "USA")
                    .Select(x => x.Name)
                    .ToList();
            }
        }
    }
}