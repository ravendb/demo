using System.Linq;
using DemoServer.Entities;
using Raven.Client.Indexes;

namespace DemoServer.Indexes
{
    public class ProductsAndPriceAndSuplier : AbstractIndexCreationTask<Product>
    {
        public class Result
        {
            public string ProductId { get; set; }
            public decimal PricePerUnit { get; set; }
            public int UnitsInStock { get; set; }
        }

        public ProductsAndPriceAndSuplier()
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
