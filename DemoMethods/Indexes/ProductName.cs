using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class ProductName : AbstractIndexCreationTask<Product>
    {
        public ProductName()
        {
            Map = products => from product in products
                              select new
                              {
                                  product.Name
                              };
        }
    }
}