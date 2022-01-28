using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using Raven.Client.Documents.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.MultiMapIndexes.MultiMapReduceIndex
{
    public class MultiMapReduceIndexController : DemoCodeController
    {
        public MultiMapReduceIndexController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class CityCommerceDetails : AbstractMultiMapIndexCreationTask<CityCommerceDetails.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string CityName { get; set; }
                public int NumberOfCompaniesInCity { get; set; } 
                public int NumberOfSuppliersInCity { get; set; }
                public int NumberOfItemsShippedToCity { get; set; }
            }
            #endregion

            public CityCommerceDetails()
            {
                #region Step_3
                AddMap<Company>(companies => from company in companies
                    select new IndexEntry
                    {
                        CityName = company.Address.City,
                        NumberOfCompaniesInCity = 1,
                        NumberOfSuppliersInCity = 0,
                        NumberOfItemsShippedToCity = 0
                    });
                
                AddMap<Supplier>(suppliers => from supplier in suppliers
                    select new IndexEntry
                    {
                        CityName = supplier.Address.City,
                        NumberOfCompaniesInCity = 0,
                        NumberOfSuppliersInCity = 1,
                        NumberOfItemsShippedToCity = 0
                    });
                
                AddMap<Order>(orders => from order in orders
                    select new IndexEntry
                    {
                        CityName = order.ShipTo.City,
                        NumberOfCompaniesInCity = 0,
                        NumberOfSuppliersInCity = 0,
                        NumberOfItemsShippedToCity = order.Lines.Sum(x => x.Quantity)
                    });
                #endregion
                
                #region Step_4
                Reduce = results => from result in results
                    group result by result.CityName into g
                    select new
                    {
                        CityName = g.Key,
                        NumberOfCompaniesInCity = g.Sum(x => x.NumberOfCompaniesInCity),
                        NumberOfSuppliersInCity = g.Sum(x => x.NumberOfSuppliersInCity),
                        NumberOfItemsShippedToCity = g.Sum(x => x.NumberOfItemsShippedToCity)
                    };
                #endregion
            }
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new CityCommerceDetails().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            int minCompaniesCount = runParams.MinCompaniesCount?? 5;
            int minItemsCount = runParams.MinItemsCount?? 2000;

            #region Demo
            List<CityCommerceDetails.IndexEntry> commerceDetails;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_5
                commerceDetails = session.Query<CityCommerceDetails.IndexEntry, CityCommerceDetails>()
                    .Where(doc => doc.NumberOfCompaniesInCity > minCompaniesCount ||
                                  doc.NumberOfItemsShippedToCity > minItemsCount)
                    .OrderBy(x => x.CityName)
                    .ToList();

                #endregion
            }
            #endregion
                
           return Ok(commerceDetails);
        }
        
        public class RunParams
        {
            public int? MinCompaniesCount { get; set; }
            public int? MinItemsCount { get; set; }
        }
    }
}
