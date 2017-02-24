using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DemoServer.Controllers;
using DemoServer.Helpers;
using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Queries.Facets;

namespace DemoServer.Demos.Menu
{
    public partial class MenuController : BaseController
    {
        public static List<Facet> FixedFacet { get; set; }

        [HttpGet]
        [Route("/menu/CreateIndexesAndTransformers")]
        [Demo("Deploy Indexes and Transformers", DemoOutputType.String, demoOrder: 300)]
        public object CreateIndexesAndTransformers()
        {
            // Side By Side Index Creation:
            // IndexCreation.SideBySideCreateIndexes(GetType().GetTypeInfo().Assembly, DocumentStoreHolder.Store);

            try
            {
                IndexCreation.CreateIndexes(GetType().GetTypeInfo().Assembly, DocumentStoreHolder.Store);
                CreateFixedFacet();

                DocumentStoreHolder.MediaStore.ExecuteIndex(new LastFmAnalyzed());
                DocumentStoreHolder.MediaStore.ExecuteTransformer(new TransformerLastFm());
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "Indexes, Transformers and Facets were created successfully";
        }

        private static void CreateFixedFacet()
        {
            const decimal fromVal = 10;
            const decimal toVal = 20;

            FixedFacet = FacetRangeCreation.CreateFacets(fromVal, toVal);
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(new FacetSetup { Id = "facets/ProductFacet", Facets = FixedFacet });
                session.SaveChanges();
            }
        }
    }
}