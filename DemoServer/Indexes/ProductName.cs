using System.Linq;
using DemoServer.Entities;
using Raven.Client.Documents.Indexes;

namespace DemoServer.Indexes
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