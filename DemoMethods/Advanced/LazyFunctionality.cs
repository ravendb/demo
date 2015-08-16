using DemoMethods.Entities;
using Raven.Client;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Raven.Abstractions.Data;
using Raven.Client.Bundles.MoreLikeThis;
using Raven.Client.Document;
using Raven.Abstractions.Indexing;
using Raven.Database;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
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

        [HttpGet]
        public object LazyFunctionality()
        {
            new Index_Product().Execute(Store);

            using (var session = Store.OpenSession())
            {
                Lazy<Product> result = session.Advanced.Lazily.Load<Product>("products/1");
                return DemoUtilities.Instance.ObjectToJson(result);
            }
        }
    }
}
