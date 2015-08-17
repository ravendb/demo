﻿using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Client;
using Raven.Client.Listeners;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
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
