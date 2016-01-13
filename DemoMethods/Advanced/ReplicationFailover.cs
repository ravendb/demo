using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using DemoMethods.Indexes;
using Raven.Client;
using Raven.Client.Document;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : DemoApiController
    {
        [HttpGet]
        [Demo("ReplicationFailover", DemoOutputType.Flatten, demoOrder: 200)]
        public object ReplicationFailover(string id = "Users/1")
        {
            using (var store = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "Rep1"
            }.Initialize())
            {
                using (var session = store.OpenSession())
                {
                    RavenQueryStatistics stats;
                    var results = session.Load<dynamic>(id);

                   
                    return results;
                }
            }
        }
    }
}
