using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DemoMethods.Helpers;
using DemoMethods.Indexes;
using Raven.Abstractions.Data;

namespace DemoMethods
{
    public partial class MenuController : DemoApiController
    {
        public static List<Facet> FixedFacet { get; set; }
        public void CreateFixedFacet()
        {
            decimal fromVal = 10;
            decimal toVal = 20;
            FixedFacet = FacetRangeCreation.CreateFacets(fromVal, toVal);
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(new FacetSetup { Id = "facets/ProductFacet", Facets = FixedFacet });
                session.SaveChanges();
            }
        }
    }
}
