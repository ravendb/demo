using System;
using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Raven.Client.Documents;

namespace DemoServer.Controllers.Demos.Advanced.ReplicationFailover
{
    public class ReplicationFailoverController : DemoCodeController
    {
        public ReplicationFailoverController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache,
            MediaStoreCache mediaStoreCache, DatabaseSetup databaseSetup)
            : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        public class RunParams
        {
            public string Id { get; set; }
            public string MachineName { get; set; }
        }

        #region Demo
        private IDocumentStore GetDocumentStore(string machineName)
        {
            if (string.IsNullOrEmpty(machineName))
                machineName = Environment.MachineName;

            var documentStore = new DocumentStore
            {
                Urls = new[] { "http://" + machineName + ":8080" },
                Database = "Demo"
            };

            documentStore.Initialize();
            documentStore.SetRequestTimeout(TimeSpan.FromSeconds(1));

            return documentStore;
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var machineName = runParams.MachineName;
            var id = runParams.Id;

            dynamic results;

            using (var store = GetDocumentStore(machineName))
            using (var session = store.OpenSession())
            {
                results = session.Load<dynamic>(id);
            }

            return Ok(results);
        }
        #endregion
    }
}
