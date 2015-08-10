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
                                      product.QuantityPerUnit
                                  };
                Sort(x => x.QuantityPerUnit, SortOptions.String);
            }   
        }

        [HttpGet]
        public object DynamicAggrigation()
        {
            new Index_Products().Execute(Store);

            using (var session = Store.OpenSession())
            {
                var result = session.Query<Product, Index_Products>()
                    .AggregateBy(x => x.QuantityPerUnit)
                    .AddRanges(
                        x => x.QuantityPerUnit < 10,
                        x => x.QuantityPerUnit >= 10 && x.QuantityPerUnit < 20,
                        x => x.QuantityPerUnit >= 20
                    )
                    .SumOn(x => x.QuantityPerUnit)
                    .ToList();
                return result;
            }
        }
    }
}
