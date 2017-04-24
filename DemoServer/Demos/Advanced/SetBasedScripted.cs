using System.Collections.Generic;
using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Queries;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/setBasedScripted")]
        [Demo("Set Based Scripted", DemoOutputType.Flatten, demoOrder: 240)]
        public object SetBasedScripted(string original = "USA", string newVal = "United States of America")
        {
            var updateByIndex = DocumentStoreHolder.Store.Operations.Send(new PatchByIndexOperation(
                new CompaniesAndCountry().IndexName,
                new IndexQuery { Query = "Address_Country:" + original },
                new PatchRequest
                {
                    Script = "this.Address.Country = newVal;",
                    Values = new Dictionary<string, object> { { "newVal", newVal } }
                }));

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
    }
}
