using System;
using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using Raven.Abstractions.Data;
using Raven.Client.Bundles.MoreLikeThis;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : DemoApiController
    {
        // [HttpGet]
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

                return (mltByArtist.ToList());
            }
        }
    }
}
