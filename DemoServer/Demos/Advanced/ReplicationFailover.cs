using DemoServer.Controllers;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/replicationFailover")]
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
                    var results = session.Load<dynamic>(id);

                    return results;
                }
            }
        }
    }
}
