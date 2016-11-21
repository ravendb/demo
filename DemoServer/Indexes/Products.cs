using System.Linq;
using DemoServer.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

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

            Sort(x => x.UnitsInStock, SortOptions.NumericLong);
        }
    }

}
