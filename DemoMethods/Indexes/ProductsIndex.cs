using System.Linq;
using DemoMethods.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace DemoMethods.Indexes
{
    public class Index_Products : AbstractIndexCreationTask<Product>
    {
        public Index_Products()
        {
            Map = products => from product in products
                              select new
                              {
                                  product.PricePerUnit,
                                  product.UnitsInStock
                              };
            Sort(x => x.UnitsInStock, SortOptions.Int);
        }
    }

}
