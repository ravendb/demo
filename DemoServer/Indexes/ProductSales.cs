using System.Linq;
using DemoServer.Entities;
using Raven.Client.Indexes;

namespace DemoServer.Indexes
{
    public class ProductSales : AbstractIndexCreationTask<Order, ProductSales.Result>
    {
        public class Result
        {
            public string Product { get; set; }

            public int Count { get; set; }

            public decimal Total { get; set; }
        }

        public ProductSales()
        {
            Map = orders => from order in orders
                            from line in order.Lines
                            select new
                            {
                                Product = line.Product,
                                Count = 1,
                                Total = ((line.Quantity * line.PricePerUnit) * (1 - line.Discount))
                            };

            Reduce = results => from result in results
                                group result by result.Product into g
                                select new
                                {
                                    Product = g.Key,
                                    Count = g.Sum(x => x.Count),
                                    Total = g.Sum(x => x.Total)
                                };
        }
    }
}
