using DemoMethods.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Indexes;
using System.Linq;
using System.Web.Http;


namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
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

        [HttpGet]
        public object DynamicAggrigation()
        {
            new Index_Products().Execute(Store);

            using (var session = Store.OpenSession())
            {
                var result = session.Query<Product, Index_Products>()
                    .AggregateBy(x => x.PricePerUnit)
                    .AddRanges(
                        x => x.UnitsInStock < 10,
                        x => x.UnitsInStock >= 10 && x.UnitsInStock < 20,
                        x => x.UnitsInStock >= 20
                    )
                    .SumOn(x => x.UnitsInStock)
                    .ToList();
                    
                var result2 = session.Query<Product, Index_Products>().ToList();

                return result;
            }
        }
    }
}
