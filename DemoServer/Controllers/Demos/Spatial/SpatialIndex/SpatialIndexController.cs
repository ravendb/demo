using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.Spatial.SpatialIndex
{
    public class SpatialIndexController : DemoCodeController
    {
        public SpatialIndexController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        #region Demo
        #region Step_1
        public class Companies_ByLocation : AbstractIndexCreationTask<Company, Companies_ByLocation.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string CompanyName { get; set; }
                public object LocationCoordinates { get; set; }
            }
            #endregion

            public Companies_ByLocation()
            {
                #region Step_3
                Map = companies => from company in companies
                    select new IndexEntry
                    {
                        CompanyName = company.Name, 
                        LocationCoordinates = CreateSpatialField(company.Address.Location.Latitude, company.Address.Location.Longitude)
                    };
                #endregion
                
                #region Step_4
                Spatial("LocationCoordinates", factory => factory.Geography.QuadPrefixTreeIndex(5));
                #endregion
            }
        }
        #endregion
        
        protected override Task SetDemoPrerequisites() => new Companies_ByLocation().ExecuteAsync(DocumentStoreHolder.Store);
        
        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            List<Company> companiesNearSeattle;
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                string wktPolygon = "POLYGON ((-125.06868394091362 41.855902525062724," +
                                             " -109.99544175341362 41.888625730467275," +
                                             " -116.76301987841362 50.59949235579767,"  +
                                             " -125.26643784716362 50.592518406260766," +
                                             " -125.06868394091362 41.855902525062724))";
                #endregion

                double SeattleLatitude = 47.6062;
                double SeattleLongitude = -122.3321;
                
                #region Step_6
                companiesNearSeattle = session.Query<Companies_ByLocation.IndexEntry, Companies_ByLocation>()
                    .Spatial("LocationCoordinates",
                        spatialCriteria => spatialCriteria.Within(wktPolygon))
                    .OrderByDistance("LocationCoordinates",
                        SeattleLatitude, SeattleLongitude)
                    .OfType<Company>()
                    .ToList();
                #endregion
            }
            #endregion

            return Ok(companiesNearSeattle);
        }
    }
}
