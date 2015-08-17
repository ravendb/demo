using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class Index_Product : AbstractIndexCreationTask<Product>
    {
        public Index_Product()
        {
            Map = products => from product in products
                              select new
                              {
                                  product.Name
                              };
        }
    }
}