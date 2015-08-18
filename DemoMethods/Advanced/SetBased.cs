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
            Store.ExecuteIndex(new Index_CompaniesAndCountry());

            Store.DatabaseCommands.UpdateByIndex("Index/CompaniesAndCountry",
                new IndexQuery {Query = "Country:USA"},
                new[]
                {
                    new PatchRequest
                    {
                        Type = PatchCommandType.Modify,
                        Name = "Country",
                        Value = "United States of America"
                    }
                });            

            using (var session = Store.OpenSession())
            {
                var results = session
                    .Advanced
                    .DocumentQuery<Company, Index_CompaniesAndAddresses>()
                    .Search("Address", "USA")
                    .ToList();
                    
                return DemoUtilities.Instance.ObjectToJson(results);
            }
        }
    }
}
