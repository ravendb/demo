using System.Linq;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;
using Raven.Abstractions.Data;
using Raven.Client.Bundles.MoreLikeThis;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public object MoreLikeThis()
        {
            new IndexCategory().Execute(DocumentStoreHolder.Store);

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                Category[] products = session
                .Advanced
                .MoreLikeThis<Category>(
                "IndexCategory",
                null,
                new MoreLikeThisQuery
                {
                    IndexName = "IndexCategory",
                    DocumentId = "categories/3",
                    Fields = new[] { "Description" }
                });

                return (products.ToList());
            }
        }
    }
}
