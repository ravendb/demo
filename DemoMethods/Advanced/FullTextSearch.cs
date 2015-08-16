using DemoMethods.Entities;
using Raven.Client;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Raven.Abstractions.Data;
using Raven.Client.Bundles.MoreLikeThis;
using Raven.Client.Document;
using Raven.Abstractions.Indexing;
using Raven.Database;

namespace DemoMethods.Advanced
{
    public partial class AdvancedController : ApiController
    {
        // TODO :: implement.. 
        [HttpGet]
        public object FullTextSearch()
        {            
            // new Index_Category().Execute(Store);

            using (var session = Store.OpenSession())
            {
               

                return DemoUtilities.Instance.ObjectToJson(null);
            }
        }
    }
}
