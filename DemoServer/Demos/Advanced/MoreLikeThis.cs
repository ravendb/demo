using System.Collections.Generic;
using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Queries.MoreLikeThis;

namespace DemoServer.Demos.Advanced
{
    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/moreLikeThis")]
        public object MoreLikeThis(string documentId = "lastfm/9295")
        {
            using (var session = DocumentStoreHolder.MediaStore.OpenSession())
            {
                List<LastFm> mltByArtist = session
                    .Advanced
                    .MoreLikeThis<LastFm>(
                        new MoreLikeThisQuery
                        {
                            Query = "FROM INDEX LastFmAnalyzed",
                            DocumentId = documentId,
                            Fields = new[] { "Query" }
                        });

                return mltByArtist.ToList();
            }
        }
    }
}
