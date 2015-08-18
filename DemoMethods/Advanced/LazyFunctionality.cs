using System;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Indexes;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        [HttpGet]
        public object LazyFunctionality()
        {
            new IndexProduct().Execute(DocumentStoreHolder.Store);

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                Lazy<Product> result = session.Advanced.Lazily.Load<Product>("products/1");
                return DemoUtilities.Instance.ObjectToJson(result);
            }
        }
    }
}
