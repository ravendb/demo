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
        public class Index_NewIndex_Product : AbstractIndexCreationTask<Product>
        {
            public Index_NewIndex_Product()
            {
                Map = products => from product in products
                                  select new
                                  {
                                      product
                                  };
            }
        }

        public class QueryListener : IDocumentQueryListener
        {
            public void BeforeQueryExecuted(IDocumentQueryCustomization queryCustomization)
            {
                queryCustomization.NoCaching();
                queryCustomization.WaitForNonStaleResults();
            }
        }

        [HttpGet]
        public object Listeners()
        {
            // make sure we create new stale index
            Store.DatabaseCommands.DeleteIndex("Index/NewIndex/Product");

            Store.Listeners.RegisterListener(new QueryListener());

            Store.ExecuteIndex(new Index_NewIndex_Product());

            using (var session = Store.OpenSession())
            {
                
                var result = session.Query<Product>("Index/NewIndex/Product");
                return DemoUtilities.Instance.ObjectToJson(result.ToList());
            }
        }
    }
}
