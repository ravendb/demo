using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System;
using System.Collections.Generic;
using Raven.Client.Documents.Operations.Revisions;
#endregion

namespace DemoServer.Controllers.Demos.Revisions.EnableRevisions
{
    public class EnableRevisionsController : DemoCodeController
    {
        public EnableRevisionsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
       
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string collection1 = runParams.Collection1;
            string collection2 = runParams.Collection2;

            #region Demo
            #region Step_1
            RevisionsConfiguration myRevisionsConfiguration = new RevisionsConfiguration
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
            ConfigureRevisionsOperation revisionsConfigurationOperation = new ConfigureRevisionsOperation(myRevisionsConfiguration);
            DocumentStoreHolder.Store.Maintenance.Send(revisionsConfigurationOperation);
            #endregion
            #endregion
          
            return Ok($"Revisions on {collection1} & {collection2} collections were successfully defined");
        }
    }

    public class RunParams
    {
        public string Collection1 { get; set; }
        public string Collection2 { get; set; }
    }
}
