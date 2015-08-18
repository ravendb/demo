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
            DocumentStoreHolder.Store.ExecuteIndex(new IndexCompaniesAndCountry());

            DocumentStoreHolder.Store.DatabaseCommands.UpdateByIndex("Index/CompaniesAndCountry",
                new IndexQuery {Query = "Country:USA"},
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
                                Value = "United States of America"
                            }, 
                        }
                    }
                });

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var results = session
                    .Advanced
                    .DocumentQuery<Company, IndexCompaniesAndCountry>()
                    .Search("Country", "USA")
                    .ToList();
                    
                return DemoUtilities.Instance.ObjectToJson(results);
            }
        }
    }
}
