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
using Raven.Client.Documents.Indexes.Spatial;
#endregion

namespace DemoServer.Controllers.Demos.Spatial.SpatialQuery
{
    public class SpatialQueryController : DemoCodeController
    {
        public SpatialQueryController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        public class EmployeeDetails
        {
            public string EmployeeName { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }
        
        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int radius = runParams.Radius ?? 2;
            
            List<EmployeeDetails> queryResults = new List<EmployeeDetails>();
            #region Demo
            List<Employee> employeesWithinCircle = new List<Employee>();
                
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                double centerPointLng = -122.3150148;
                double centerPointLat = 47.63016419999999;
                
                string wktCircle = $"CIRCLE({centerPointLng} {centerPointLat} d={radius})";
                #endregion
                
                #region Step_2
                employeesWithinCircle = session.Query<Employee>()
                    .Spatial(
                #endregion
                        #region Step_3
                        spatialField => spatialField.Point(x => x.Address.Location.Latitude,
                                                           x => x.Address.Location.Longitude),
                        #endregion
                        
                        #region Step_4
                        spatialCriteria => spatialCriteria.RelatesToShape(wktCircle,
                                                                          SpatialRelation.Within,
                                                                          SpatialUnits.Miles))
                        #endregion
                    #region Step_5
                    .OrderByDistance(
                        spatialField => spatialField.Point(x => x.Address.Location.Latitude,
                                                           x => x.Address.Location.Longitude),
                                                           centerPointLat,
                                                           centerPointLng)
                    #endregion
                    #region Step_6
                    .ToList();
                    #endregion 
            }
            #endregion

            foreach (var item in employeesWithinCircle)
            {
                var detailedItem = new EmployeeDetails()
                {
                    EmployeeName = item.FirstName + ' ' + item.LastName,
                    Longitude = item.Address.Location.Longitude,
                    Latitude = item.Address.Location.Latitude
                };
                
                queryResults.Add(detailedItem);
            } 
            
            return Ok(queryResults);
        }
        
        public class RunParams
        {
            public int? Radius { get; set; }
        }
    }
}
