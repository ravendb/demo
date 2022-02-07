using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Queries.Highlighting;
#endregion

namespace DemoServer.Controllers.Demos.TextSearch.HighlightQueryResultsMapReduce
{
    public class HighlightQueryResultsMapReduceController : DemoCodeController
    {
        public HighlightQueryResultsMapReduceController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        #region Step_1
        public class ArtistsAllSongs : AbstractIndexCreationTask<LastFm, ArtistsAllSongs.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string Artist { get; set; }
                public string AllSongTitles { get; set; }
            }
            #endregion
           
            public ArtistsAllSongs()
            {
                #region Step_3
                Map = songs => from song in songs
                    select new IndexEntry
                    {
                        Artist = song.Artist,
                        AllSongTitles = song.Title
                    };
                #endregion

                #region Step_4
                Reduce = results => from result in results
                    group result by result.Artist into g
                    select new IndexEntry
                    {
                        Artist = g.Key,
                        AllSongTitles = string.Join(" ", g.Select(x => x.AllSongTitles))
                    };
                #endregion 
                
                #region Step_5
                Store(x => x.Artist, FieldStorage.Yes);
                
                Store(x => x.AllSongTitles, FieldStorage.Yes);
                Index(x => x.AllSongTitles, FieldIndexing.Search);
                TermVector(x => x.AllSongTitles, FieldTermVector.WithPositionsAndOffsets);
                #endregion
            }
        }
        #endregion
        
        protected override async Task SetDemoPrerequisites()
        {
            await DatabaseSetup.EnsureMediaDatabaseExists(UserId);
            await new ArtistsAllSongs().ExecuteAsync(DocumentStoreHolder.MediaStore);
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string searchTerm = runParams.SearchTerm?? "smile";
            string preTag = runParams.PreTag?? " (: ";
            string postTag = runParams.PostTag?? " :) ";
            int fragmentLength = runParams.FragmentLength?? 80;
            int fragmentCount = runParams.FragmentCount?? 1;

            Highlightings highlightingsInfo;
            
            #region Demo
            List<ArtistsAllSongs.IndexEntry> artistsResults;
         
            using (IDocumentSession session = DocumentStoreHolder.MediaStore.OpenSession())
            {
                #region Step_6
                HighlightingOptions highlightOptions = new HighlightingOptions
                {
                    GroupKey = "Artist",
                    PreTags = new[] { preTag },
                    PostTags = new[] { postTag }
                };
                #endregion
                
                #region Step_7
                artistsResults = session.Query<ArtistsAllSongs.IndexEntry, ArtistsAllSongs>()
                    .Highlight(x => x.AllSongTitles,fragmentLength, fragmentCount, highlightOptions, out highlightingsInfo)
                    .Search(x => x.AllSongTitles, searchTerm)
                    .ToList();
                #endregion
                
                #region Step_8
                if (artistsResults.Count > 0)
                {
                    string[] songsFragments = highlightingsInfo.GetFragments(artistsResults[0].Artist);
                }
                #endregion
            }
            #endregion

            List<DataToShow> highlightResults = new List<DataToShow>();
            
            foreach (var artistItem in artistsResults)
            {
                string[] songsFragments = highlightingsInfo.GetFragments(artistItem.Artist);
                foreach (var fragment in songsFragments)
                {
                    DataToShow itemResults = new DataToShow
                    {
                        Artist = artistItem.Artist,
                        SongFragment = fragment
                    };
    
                    highlightResults.Add(itemResults);
                }
            }
            
            return Ok(highlightResults.OrderBy(x => x.Artist));
        }
        
        public class RunParams
        {
            public string SearchTerm { get; set; }
            public string PreTag { get; set; }
            public string PostTag { get; set; }
            public int? FragmentLength { get; set; }
            public int? FragmentCount { get; set; }
        }

        private class DataToShow
        {
            public string Artist { get; set; }
            public string SongFragment { get; set; }
        }
    }
}
