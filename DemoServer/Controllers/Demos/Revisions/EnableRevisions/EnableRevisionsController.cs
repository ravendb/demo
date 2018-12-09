using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents.Operations.Revisions;
#endregion

namespace DemoServer.Controllers.Demos.Revisions.EnableRevisions
{
    public class EnableRevisionsController : DemoCodeController
    {
        public EnableRevisionsController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseAccessor databaseAccessor) : base(headersAccessor, documentStoreCache, databaseAccessor)
        {
        }
       
        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var collection1 = runParams.Collection1;
            var collection2 = runParams.Collection2;

            #region Demo
          
            #region Step_1
            var myRevisionsConfiguration = new RevisionsConfiguration
            #endregion
            {
                #region Step_2
                Default = new RevisionsCollectionConfiguration
                {
                    Disabled = false,
                    PurgeOnDelete = false,
                    MinimumRevisionsToKeep = 5,
                    MinimumRevisionAgeToKeep = TimeSpan.FromDays(14),
                },
                #endregion
                
                #region Step_3
                Collections = new Dictionary<string, RevisionsCollectionConfiguration>
                {
                    {collection1, new RevisionsCollectionConfiguration {Disabled = true}},
                    {collection2, new RevisionsCollectionConfiguration {PurgeOnDelete = true}}
                }
                #endregion
            };
           
            #region Step_4
            var revisionsConfigurationOperation = new ConfigureRevisionsOperation(myRevisionsConfiguration);
            DocumentStoreHolder.Store.Maintenance.Send(revisionsConfigurationOperation);
            #endregion
            #endregion
          
            return Ok($"Revisions on {collection1} & {collection2} collections were successfully enabled");
        }
    }

    public class RunParams
    {
        public string Collection1 { get; set; }
        public string Collection2 { get; set; }
    }
}
