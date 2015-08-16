using DemoMethods.Entities;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMethods.Indexes
{
    public class Index_ProductsAndPriceAndSuplier : AbstractIndexCreationTask<Product>
    {
        public class Result
        {
            public string ProductId { get; set; }
            public decimal PricePerUnit { get; set; }
            public int UnitsInStock { get; set; }
        }

        public Index_ProductsAndPriceAndSuplier()
        {
            Map = products => from product in products
                              select new Result
                              {
                                  ProductId = product.Id,
                                  PricePerUnit = product.PricePerUnit,
                                  UnitsInStock = product.UnitsInStock
                              };
        }

    }
}
