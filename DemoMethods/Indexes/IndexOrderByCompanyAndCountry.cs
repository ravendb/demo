using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;
using Raven.Client.Linq.Indexing;

namespace DemoMethods.Indexes
{
    public class IndexOrderByCompanyAndCountry : AbstractIndexCreationTask<Order>
    {
        public IndexOrderByCompanyAndCountry()
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
