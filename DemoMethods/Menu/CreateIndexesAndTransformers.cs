using System;
using System.Reflection;
using System.Web.Http;
using DemoMethods.Helpers;
using DemoMethods.Indexes;
using Raven.Abstractions.Data;
using Raven.Client.Indexes;

namespace DemoMethods
{
    public partial class MenuController : DemoApiController
    {
        [HttpGet]
        public object CreateIndexesAndTransformers()
        {
            // Side By Side Index Creation:
            // IndexCreation.SideBySideCreateIndexes(Assembly.GetExecutingAssembly(), DocumentStoreHolder.Store);

            try
            {
                IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), DocumentStoreHolder.Store);

                CreateFixedFacet();
            }
            catch (Exception e)
            {
                return e;
            }

            return ("Indexes, Transformers and Facets were created successfully");
        }


       
    }
}