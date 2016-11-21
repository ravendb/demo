using System.Linq;
using DemoServer.Entities;
using Raven.Client.Indexes;

namespace DemoServer.Indexes
{
    public class OrderByCompanyAndCountry : AbstractIndexCreationTask<Order>
    {
        public OrderByCompanyAndCountry()
        {
            Map = orders => from order in orders
                            select new
                            {
                                ShipTo_City = order.ShipTo.City,
                                ShipTo_Country = order.ShipTo.Country
                            };
        }
    }
}
