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
        public class Song_TextData : AbstractIndexCreationTask<LastFm, Song_TextData.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string SongData { get; set; }
            }
            #endregion
           
            public Song_TextData()
            {
                #region Step_3
                Map = songs => from song in songs
                    select new
                    {
                        SongData = new object[]
                        {
                            song.Artist,
                            song.Title,
                            song.Tags,
                            song.TrackId,
                        }
                    };
                #endregion

                #region Step_4
                Index(x => x.SongData, FieldIndexing.Search);
                #endregion
            }
        }
        #endregion

        protected override async Task SetDemoPrerequisites()
        {
            await DatabaseSetup.EnsureMediaDatabaseExists(UserId);
            await new Song_TextData().ExecuteAsync(DocumentStoreHolder.MediaStore);
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string searchTerm = runParams.SearchTerm?? "Floyd";

            #region Demo
            List<LastFm> results;
         
            using (IDocumentSession session = DocumentStoreHolder.MediaStore.OpenSession())
            {
                #region Step_5
                results = session.Query<Song_TextData.IndexEntry, Song_TextData>()
                    .Search(x => x.SongData, searchTerm)
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
