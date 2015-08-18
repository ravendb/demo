using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class IndexProduct : AbstractIndexCreationTask<Product>
    {
        public IndexProduct()
        {
            Map = products => from product in products
                              select new
                              {
                                  product.Name
                              };
        }
    }
}