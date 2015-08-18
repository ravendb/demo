using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class IndexProductsAndPriceAndSuplier : AbstractIndexCreationTask<Product>
    {
        public class Result
        {
            public string ProductId { get; set; }
            public decimal PricePerUnit { get; set; }
            public int UnitsInStock { get; set; }
        }

        public IndexProductsAndPriceAndSuplier()
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
