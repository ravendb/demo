using System.Threading.Tasks;
using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos.Basics.EditDocument
{
    public class EditDocumentController : DemoCodeController
    {
        private const string DocumentId = "products/34-A";

        public EditDocumentController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Run(RunParams runParams)
        {
            var pricePerUnit = runParams.PricePerUnit;
            var phone = runParams.Phone;

            #region Demo
            
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                var product = session
                    .Include<Product>(x => x.Supplier)
                    .Load<Product>("products/34-A");

                var supplier = session.Load<Supplier>(product.Supplier);
                #endregion
                
                #region Step_2
                product.PricePerUnit = pricePerUnit;

                supplier.Phone = phone;
                #endregion
                
                #region Step_3
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
