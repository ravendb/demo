using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

#region Usings
using System.Linq;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.TextSearch.FullTextSearchWithStaticIndex
{
    public class FullTextSearchWithStaticIndexController : DemoCodeController
    {
        public FullTextSearchWithStaticIndexController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        protected override Task SetDemoPrerequisites() => DatabaseSetup.EnsureMediaDatabaseExists(UserId);

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
                            song.Tags,
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
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var searchTerm = runParams.SearchTerm;
            
            new LastFmAnalyzed().Execute(DocumentStoreHolder.MediaStore);
         
            using (var session = DocumentStoreHolder.MediaStore.OpenSession())
            {
                #region Step_5
                var results = session.Query<LastFmAnalyzed.Result, LastFmAnalyzed>()
                    .Take(20)
                    .Search(x => x.Query, searchTerm)
                    .As<LastFm>()
                    .ToList();
                #endregion
            }
            
            //TODO 1: How to show results ? 
            //TODO 2: Split the demo region to 2 parts, so that we have more control over what is shown !
            return Ok("Query results are: ... TODO: Show Query Results ..."); 
        }
        #endregion
        
        public class RunParams
        {
            public string SearchTerm { get; set; }
        }
    }
}
