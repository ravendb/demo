using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.RelatedDocuments.LoadRelatedDocuments
{
    public class LoadRelatedDocumentsController : DemoCodeController
    {
        private const string DocumentId = "products/34-A";

        public LoadRelatedDocumentsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            decimal pricePerUnit = runParams.PricePerUnit;
            string phone = runParams.Phone;

            #region Demo
            
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                Product product = session
                    .Include<Product>(x => x.Supplier)
                    .Load<Product>("products/34-A");
                #endregion
                
                #region Step_2
                Supplier supplier = session.Load<Supplier>(product.Supplier);
                #endregion
                
                #region Step_3
                product.PricePerUnit = pricePerUnit;
                supplier.Phone = phone;
                #endregion
                
                #region Step_4
                session.SaveChanges();
                #endregion
            }
            #endregion
            
            return Ok($"Document {DocumentId} was edited successfully");
        }

        public class RunParams
        {
            public decimal PricePerUnit { get; set; }

            public string Phone { get; set; }
        }
    }
}
