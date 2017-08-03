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
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                List<LastFm> mltByArtist = session
                    .Advanced
                    .MoreLikeThis<LastFm>(
                        new MoreLikeThisQuery
                        {
                            Query = "FROM INDEX IndexFullTextSearch",
                            DocumentId = documentId,
                            Fields = new[] { "Artist" }
                        });

                return mltByArtist.ToList();
            }
        }
    }
}
