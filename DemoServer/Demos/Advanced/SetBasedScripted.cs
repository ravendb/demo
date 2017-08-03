using System.Collections.Generic;
using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Queries;
using System;

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
                   new IndexQuery
                   {
                       Query = "FROM Companies WHERE Address.Country = :country",
                       QueryParameters = new Raven.Client.Parameters()
                       {
                           ["country"] = original
                       }
                   },
                   new PatchRequest
                   {
                       Script = "this.Address.Country = newVal;",
                       Values = new Dictionary<string, object> { { "newVal", newVal } }
                   }));

                updateByIndex.WaitForCompletion();

                using (var session = DocumentStoreHolder.Store.OpenSession())
                {
                    var results = session
                        .Query<Company>()
                        .Customize(x => x.WaitForNonStaleResultsAsOfNow())
                        .Where(x => x.Address.Country == newVal)
                        .ToList();

                    return (results);
                }
            
        }
    }
}
