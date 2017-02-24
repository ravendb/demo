using System.Linq;
using DemoServer.Entities;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Linq.Indexing;

namespace DemoServer.Indexes
{
    public class OrderByCompanyAndCountryWithBoost : AbstractIndexCreationTask<Order>
    {
        public OrderByCompanyAndCountryWithBoost()
        {
            Map = orders => from order in orders
                            select new
                            {
                                ShipTo_City = order.ShipTo.City.Boost(10),
                                ShipTo_Country = order.ShipTo.Country
                            };
        }
    }
}
