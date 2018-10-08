export const usings = `using DemoServer.Indexes;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Queries.Facets;`;

export const sourceCode = `namespace DemoServer.Demos.Menu
{
    public partial class MenuController : BaseController
    {
        public static List<FacetBase> FixedFacet { get; set; }

        [HttpGet]
        [Route("/menu/CreateIndexes")]
        [Demo("Deploy Indexes", DemoOutputType.String, demoOrder: 310)]
        public object CreateIndexes()
        {
            // Side By Side Index Creation:
            // IndexCreation.SideBySideCreateIndexes(GetType().GetTypeInfo().Assembly, DocumentStoreHolder.Store);

            try
            {
                IndexCreation.CreateIndexes(GetType().GetTypeInfo().Assembly, DocumentStoreHolder.Store);
                CreateFixedFacet();

                DocumentStoreHolder.MediaStore.ExecuteIndex(new LastFmAnalyzed());
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "Indexes and Facets were created successfully";
        }

        private static void CreateFixedFacet()
        {
            const decimal fromVal = 10;
            const decimal toVal = 20;

            FixedFacet = FacetRangeCreation.CreateFacets(fromVal, toVal);

            var facets = FacetRangeCreation.CreateFacets();
            var rangeFacets = FacetRangeCreation.CreateRangeFacets(fromVal, toVal);

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(new FacetSetup
                {
                    Id = "facets/ProductFacet",
                    Facets = facets,
                    RangeFacets = rangeFacets
                });

                session.SaveChanges();
            }
        }
    }
}`;