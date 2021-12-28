using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Linq;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents.Indexes;
#endregion

namespace DemoServer.Controllers.Demos.MultiMapIndexes.MultiMapIndexBasic
{
    public class MultiMapIndexBasicController : DemoCodeController
    {
        public MultiMapIndexBasicController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }
        
        #region Demo
        #region Step_1
        public class CompaniesAndSuppliers_ByName : AbstractMultiMapIndexCreationTask<CompaniesAndSuppliers_ByName.IndexEntry>
        #endregion
        {
            #region Step_2
            public class IndexEntry
            {
                public string Name { get; set; }
            }
            #endregion
            
            #region Step_3
            public CompaniesAndSuppliers_ByName()
            {
                AddMap<Company>(companies => from company in companies
                    select new IndexEntry
                    {
                        Name = company.Name
                    });
                
                AddMap<Supplier>(suppliers => from supplier in suppliers
                    select new IndexEntry
                    {
                        Name = supplier.Name
                    });
            }
            #endregion
        }
        #endregion

        protected override Task SetDemoPrerequisites() => new CompaniesAndSuppliers_ByName().ExecuteAsync(DocumentStoreHolder.Store);

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string namePrefix = runParams.NamePrefix?? "A";

            #region Demo
            List<CompaniesAndSuppliers_ByName.IndexEntry> companiesAndSuppliersNames;

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_4
                companiesAndSuppliersNames = session.Query<CompaniesAndSuppliers_ByName.IndexEntry, CompaniesAndSuppliers_ByName>()
                    .Where(doc => doc.Name.StartsWith(namePrefix))
                    .ToList();

                #endregion
            }
            #endregion
                
           return Ok(companiesAndSuppliersNames);
        }
        
        public class RunParams
        {
            public string NamePrefix { get; set; }
        }
    }
}
