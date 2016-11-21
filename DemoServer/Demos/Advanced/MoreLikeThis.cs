using System.Linq;
using DemoServer.Controllers;
using DemoServer.Entities;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Bundles.MoreLikeThis;
using Raven.Client.Data.Queries;

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
                LastFm[] mltByArtist = session
                    .Advanced
                    .MoreLikeThis<LastFm>(
                        "IndexFullTextSearch",
                        null,
                        new MoreLikeThisQuery
                        {
                            IndexName = "IndexFullTextSearch",
                            DocumentId = documentId,
                            Fields = new[] { "Artist" }
                        });

                return mltByArtist.ToList();
            }
        }
    }
}
