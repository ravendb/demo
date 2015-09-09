using System.Collections.Specialized;
using System.Linq;
using System.Web.Http;
using DemoMethods.Indexes;
using Raven.Abstractions.Data;
using DemoMethods.Entities;
using Raven.Client.Connection;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public object SetBased(string original = "USA", string newVal = "United States of America")
        {
            var updateByIndex = DocumentStoreHolder.Store.DatabaseCommands.UpdateByIndex(new IndexCompaniesAndCountry().IndexName,
                new IndexQuery { Query = "Address_Country:" + original },
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
                                Value = newVal
                            }, 
                        }
                    }
                });

            updateByIndex.WaitForCompletion();


            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var results = session
                    .Query<IndexCompaniesAndCountry.Result, IndexCompaniesAndCountry>()
                    .Customize(x => x.WaitForNonStaleResultsAsOfNow())
                    .Where(x => x.Address_Country == newVal)
                    .OfType<Company>()
                    .ToList();

                return (results);
            }
        }
    }
}
