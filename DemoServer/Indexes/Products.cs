using System.Linq;
using DemoServer.Entities;
using Raven.Client.Documents.Indexes;

namespace DemoServer.Indexes
{
    public class Products : AbstractIndexCreationTask<Product>
    {
        public Products()
        {
            Map = products => from product in products
                              select new
                              {
                                  product.Category,
                                  product.UnitsInStock
                              };

        }
    }

}
