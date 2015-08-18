using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;
using Raven.Client.Linq.Indexing;

namespace DemoMethods.Indexes
{
    public class IndexOrderByCompanyAndShipper : AbstractIndexCreationTask<Order>
    {
        public IndexOrderByCompanyAndShipper()
        {
            Map = orders => from order in orders
                            select new
                            {
                                CompanyId = order.Company.Boost(10),
                                ShipperId = order.ShipVia
                            };
        }
    }
}
