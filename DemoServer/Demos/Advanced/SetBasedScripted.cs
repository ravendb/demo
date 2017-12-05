using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
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
        public object SetBasedScripted(string employee = "employees/1", int discount = 5)
        {
            var updateByIndex = DocumentStoreHolder.Store.Operations.Send(new PatchByQueryOperation(
                new IndexQuery
                {
                    Query = @"
FROM INDEX 'Orders/Totals' 
WHERE Employee = $emp 
UPDATE {
    for(var i = 0; i < this.Lines.length; i++)
    {
        this.Lines[i].Discount = Math.max(this.Lines[i].Discount || 0, args.discount);
    }
}",
                    QueryParameters = new Raven.Client.Parameters()
                    {
                        ["emp"] = employee,
                        ["discount"] = discount
                    }
                }));

            updateByIndex.WaitForCompletion();

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var results = session
                    .Query<Order>()
                    .Customize(x => x.WaitForNonStaleResults())
                    .Where(x => x.Employee == employee)
                    .ToList();

                return (results);
            }
        }
    }
}
