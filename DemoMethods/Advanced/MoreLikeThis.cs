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
                "Index/Category",
                null,
                new MoreLikeThisQuery
                {
                    IndexName = "Index/Category",
                    DocumentId = "categories/3",
                    Fields = new[] { "Description" }
                });

                return DemoUtilities.Instance.ObjectToJson(products.ToList());
            }
           // TODO :: this is a bad example.. need article-body like example
        }
    }
}
