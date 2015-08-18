using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Client;

namespace DemoMethods.Basic
{
    public partial class BasicController : ApiController
    {       
        [HttpGet]
        public object DynamicAggrigation()
        {
            new IndexProducts().Execute(DocumentStoreHolder.Store);

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var result = session.Query<Product, IndexProducts>()
                    .AggregateBy(x => x.PricePerUnit)
                    .AddRanges(
                        x => x.UnitsInStock < 10,
                        x => x.UnitsInStock >= 10 && x.UnitsInStock < 20,
                        x => x.UnitsInStock >= 20
                    )
                    .SumOn(x => x.UnitsInStock)
                    .ToList();
                    
                var result2 = session.Query<Product, IndexProducts>().ToList();

                return result;
            }
        }
    }
}
