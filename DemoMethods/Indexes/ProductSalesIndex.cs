using DemoMethods.Entities;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMethods.Indexes
{
    public class Product_Sales : AbstractIndexCreationTask<Order, Product_Sales.Result>
    {
        public class Result
        {
            public string Product { get; set; }

            public int Count { get; set; }

            public decimal Total { get; set; }
        }

        public Product_Sales()
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
