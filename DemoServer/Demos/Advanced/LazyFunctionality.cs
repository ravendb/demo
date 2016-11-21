using System;
using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Client;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/lazyFunctionality")]
        [Demo("Lazy Functionality", DemoOutputType.Flatten, demoOrder: 220)]
        public object LazyFunctionality(string companyId = "companies/20")
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                Lazy<Company> company = session.Advanced.Lazily.Load<Company>(companyId);
                Lazy<int> countOfOrders = session.Query<Order>().Where(x => x.Company == companyId).CountLazily();

                session.Advanced.Eagerly.ExecuteAllPendingLazyOperations();

                return new
                {
                    CompanyName = company.Value.Name,
                    NumberOfOrders = countOfOrders.Value,
                    session.Advanced.NumberOfRequests
                };
            }
        }
    }
}
