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
        public class Index_Category : AbstractIndexCreationTask<Category>
        {
            public Index_Category()
            {
                Map = categories => from category in categories
                                  select new
                                  {
                                      category.Description
                                  };

                Stores.Add(x => x.Description, FieldStorage.Yes);
                Analyzers.Add(x => x.Description, "Lucene.Net.Analysis.Standard.StandardAnalyzer");
            }
        }

        [HttpGet]
        public object MoreLikeThis()
        {            
            new Index_Category().Execute(Store);

            using (var session = Store.OpenSession())
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
