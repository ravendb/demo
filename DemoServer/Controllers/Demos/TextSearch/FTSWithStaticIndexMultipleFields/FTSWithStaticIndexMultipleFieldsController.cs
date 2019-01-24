using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.TextSearch.FTSWithStaticIndexMultipleFields
{
    public class FTSWithStaticIndexMultipleFieldsController : DemoCodeController
    {
        public FTSWithStaticIndexMultipleFieldsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        #region Step_1
        public class LastFmAnalyzed : AbstractIndexCreationTask<LastFm, LastFmAnalyzed.Result>
        #endregion
        {
            #region Step_2
            public class Result
            {
                public string Query { get; set; }
            }
            #endregion
           
            public LastFmAnalyzed()
            {
                #region Step_3
                Map = songs => from song in songs
                    select new
                    {
                        Query = new object[]
                        {
                            song.Artist,
                            song.TimeStamp,
                            song.Title,
                            song.TrackId
                        }
                    };
                #endregion

                #region Step_4
                Index(x => x.Query, FieldIndexing.Search);
                #endregion
            }
        }
        #endregion

        protected override async Task SetDemoPrerequisites()
        {
            await DatabaseSetup.EnsureMediaDatabaseExists(UserId);
            await new LastFmAnalyzed().ExecuteAsync(DocumentStoreHolder.MediaStore);
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string searchTerm = runParams.SearchTerm;

            #region Demo
            List<LastFm> results;
         
            using (IDocumentSession session = DocumentStoreHolder.MediaStore.OpenSession())
            {
                #region Step_5
                results = session.Query<LastFmAnalyzed.Result, LastFmAnalyzed>()
                    .Search(x => x.Query, searchTerm)
                    .Take(20)
                    .As<LastFm>()
                    .ToList();
                #endregion
            }
            #endregion

            return Ok(results);
        }
        
        public class RunParams
        {
            public string SearchTerm { get; set; }
        }
    }
}
