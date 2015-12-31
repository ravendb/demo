using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using DemoMethods.Indexes;
using Raven.Abstractions.Data;
using Raven.Client;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : DemoApiController
    {
        [HttpGet]
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
