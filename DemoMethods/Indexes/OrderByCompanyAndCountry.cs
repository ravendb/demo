using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
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
