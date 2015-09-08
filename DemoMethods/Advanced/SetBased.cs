using System.Collections.Specialized;
using System.Linq;
using System.Web.Http;
using DemoMethods.Indexes;
using Raven.Abstractions.Data;
using DemoMethods.Entities;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public object SetBased()
        {
            var userParams = new NameValueCollection
                {
                    {"Original", "USA"},
                    {"New", "United States of America"}
                };
            DemoUtilities.GetUserParameters(Request.RequestUri.Query, userParams);

            DocumentStoreHolder.Store.ExecuteIndex(new IndexCompaniesAndCountry());

            DocumentStoreHolder.Store.DatabaseCommands.UpdateByIndex("Index/CompaniesAndCountry",
                new IndexQuery {Query = "Country:" + userParams["Origianl"]},
                new[]
                {
                    new PatchRequest
                    {
                        Type = PatchCommandType.Modify,
                        Name = "Address",
                        Nested = new[]
                        {
                            new PatchRequest
                            {
                                Type = PatchCommandType.Set,
                                Name = "Country",
                                Value = userParams["New"]
                            }, 
                        }
                    }
                });

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var results = session
                    .Advanced
                    .DocumentQuery<Company, IndexCompaniesAndCountry>()
                    .Search("Country", userParams["New"])
                    .ToList();
                    
                return (results);
            }
        }
    }
}
