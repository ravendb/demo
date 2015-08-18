using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using Raven.Abstractions.Data;
using Raven.Client.Indexes;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        public class ResultsType
        {
            public Product[] Page1;
            public Product[] Page5;
        }

        [HttpGet]
        public object StreamingApi_Page()
        {
            Store.ExecuteIndex(new IndexManyProduct());

            using (var session = Store.OpenSession())
            {
                var query = session.Query<Product>("Index/ManyProduct");

                var e = session
                    .Advanced                  
                    .Stream<Product>(query);

                const int pageSize = 3;
                
                ResultsType results = new ResultsType();
                results.Page1 = new Product[pageSize];
                results.Page5 = new Product[pageSize];

                // Extract page #1 and #5 for the example..
                for (var page = 1; page <= 10; page++)
                {
                    for (var itemInPage = 0; itemInPage < pageSize; itemInPage++)
                    {
                        e.MoveNext();
                        var product = e.Current;

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
