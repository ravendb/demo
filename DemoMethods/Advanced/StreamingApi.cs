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
using Raven.Client.Listeners;
using Raven.Abstractions.Indexing;
using Raven.Database;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        public class Index_ManyProduct : AbstractIndexCreationTask<Product>
        {
            public Index_ManyProduct()
            {
                Map = products => from product in products
                                  select new
                                  {
                                      product
                                  };
            }
        }

        public class ResultsType
        {
            public Product[] Page1;
            public Product[] Page5;
        }

        [HttpGet]
        public object StreamingApi_Page()
        {
            Store.ExecuteIndex(new Index_ManyProduct());

            using (var session = Store.OpenSession())
            {
                var query = session.Query<Product>("Index/ManyProduct");

                IEnumerator<StreamResult<Product>> e = session
                    .Advanced                  
                    .Stream<Product>(query);

                const int PAGE_SIZE = 3;
                
                ResultsType results = new ResultsType();
                results.Page1 = new Product[PAGE_SIZE];
                results.Page5 = new Product[PAGE_SIZE];

                // Extract page #1 and #5 for the example..
                for (int page = 1; page <= 10; page++)
                {
                    for (int itemInPage = 0; itemInPage < PAGE_SIZE; itemInPage++)
                    {
                        e.MoveNext();
                        StreamResult<Product> product = e.Current;

                        if (page == 1)
                            results.Page1[itemInPage] = product.Document;
                        if (page == 5)
                            results.Page5[itemInPage] = product.Document;
                    }
                }

                return DemoUtilities.Instance.ObjectToJson(results);
            }
        }        
    }
}
