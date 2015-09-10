using System;
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
        public object SetBased(string original = "USA", string newVal = "United States of America")
        {
            try
            {
                var updateByIndex = DocumentStoreHolder.Store.DatabaseCommands.UpdateByIndex(new CompaniesAndCountry().IndexName,
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
                        .Query<CompaniesAndCountry.Result, CompaniesAndCountry>()
                        .Customize(x => x.WaitForNonStaleResultsAsOfNow())
                        .Where(x => x.Address_Country == newVal)
                        .OfType<Company>()
                        .ToList();

                    return (results);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
