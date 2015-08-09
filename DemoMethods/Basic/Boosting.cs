using DemoMethods.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq.Indexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {
        public class Index_OrderByCompanyAndShipper : AbstractIndexCreationTask<Order>
        {
            public Index_OrderByCompanyAndShipper()
            {
                Map = orders => from order in orders
                                select new
                                {
                                    CompanyId = order.Company.Boost(10),
                                    ShipperId = order.ShipVia
                                };
            }
        }
        
        [HttpGet]
        public object Boosting()
        {
            using (var session = Store.OpenSession())
            {
                new Index_OrderByCompanyAndShipper().Execute(Store);

                var orders = session.Query<Order,Index_OrderByCompanyAndShipper>()
                .ToList();

                return DemoUtilities.Instance.ObjectToJson(orders);
            }
        }
    }
}
