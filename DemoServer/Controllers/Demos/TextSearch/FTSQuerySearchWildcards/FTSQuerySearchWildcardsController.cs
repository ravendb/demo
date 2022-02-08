using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Queries;
#endregion

namespace DemoServer.Controllers.Demos.TextSearch.FTSQuerySearchWildcards
{
    public class FTSQuerySearchWildcardsController : DemoCodeController
    {
        public FTSQuerySearchWildcardsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string start = runParams.Start?? "ma";
            string end = runParams.End?? "lin";
            string middle = runParams.Middle?? "oliv";
            int numberOfResults = runParams.NumberOfResults?? 10;
            
            #region Demo
            List<LastFm> songsWithMatchingTerms;

            using (IDocumentSession session = DocumentStoreHolder.MediaStore.OpenSession())
            {
                #region Step_1
                songsWithMatchingTerms = session.Query<LastFm>()
                    #endregion
                    #region Step_2
                    .Search(x => x.Artist, $"{start}* *{end}", @operator: SearchOperator.And)
                    #endregion
                    #region Step_3
                    .Search(x => x.Title, $"*{middle}*")
                    #endregion
                    #region Step_4
                    .Take(numberOfResults)
                    .OrderBy(x => x.Artist)
                    #endregion
                    #region Step_5
                    .ToList();
                    #endregion
            }
            #endregion 
            
            return Ok(songsWithMatchingTerms);
        }
        
        public class RunParams
        {
            public string Start { get; set; }
            public string End { get; set; }
            public string Middle { get; set; }
            public int? NumberOfResults { get; set; }
        }
    }
}
