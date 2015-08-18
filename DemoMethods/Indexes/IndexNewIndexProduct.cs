using System.Linq;
using DemoMethods.Entities;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class IndexNewIndexProduct : AbstractIndexCreationTask<Product>
    {
        public IndexNewIndexProduct()
        {
            Map = products => from product in products
                              select new
                              {
                                  product
                              };
        }
    }
}