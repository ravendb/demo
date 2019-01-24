using System;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
#region Usings
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Advanced.ReplicationFailover
{
    public class ReplicationFailoverController : DemoCodeController
    {
        public ReplicationFailoverController(UserIdContainer userId, UserStoreCache userStoreCache,
            MediaStoreCache mediaStoreCache, DatabaseSetup databaseSetup)
            : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        public class FailoverStoreHolder
        {
            public static string MachineName { get; set; }

            private static readonly Lazy<IDocumentStore> _store = new Lazy<IDocumentStore>(CreateDocumentStore);

            private static IDocumentStore CreateDocumentStore()
            {
                string machineName = string.IsNullOrEmpty(MachineName)
                    ? Environment.MachineName
                    : MachineName;

                IDocumentStore documentStore = new DocumentStore
                {
                    Urls = new[] { "http://" + machineName + ":8080" },
                    Database = "Demo",
                };

                documentStore.Initialize();
                documentStore.SetRequestTimeout(TimeSpan.FromSeconds(1));

                return documentStore;
            }

            public static IDocumentStore Store => _store.Value;
        }
        #endregion

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string id = runParams.Id;
            var machineName = runParams.MachineName;

            #region Demo
            FailoverStoreHolder.MachineName = machineName;

            dynamic results;

            using (IDocumentSession session = FailoverStoreHolder.Store.OpenSession())
            {
                results = session.Load<dynamic>(id);
            }
            #endregion

            return Ok(results);
        }
    }

    public class RunParams
    {
        public string Id { get; set; }
        public string MachineName { get; set; }
    }
}
