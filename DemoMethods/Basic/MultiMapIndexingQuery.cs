using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        [HttpGet]
        public object MultiMapIndexingQuery(string country = "USA")
        {
            try
            {
                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    return session.Query<NameAndCountry.Result, NameAndCountry>()
                        .Customize(x => x.WaitForNonStaleResults())
                        .Search(x => x.Country, country)
                        .Select(x => x.Name)
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