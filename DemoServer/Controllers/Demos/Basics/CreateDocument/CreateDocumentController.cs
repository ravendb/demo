using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos.Basics.CreateDocument
{
    public class CreateDocumentController : DemoCodeController
    {
        public CreateDocumentController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var supplierName = runParams.SupplierName;
            var supplierPhone = runParams.SupplierPhone;
            var productName = runParams.ProductName;

            #region Demo 
            #region Step_1
            var supplier = new Supplier
            {
                Name = supplierName,
                Phone = supplierPhone
            };

            var category = new Category
            {
                Name = "Videos",
                Description = "DVD, Bluray etc."
            };

            var product = new Product
            {
                Name = productName
            };
            #endregion

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_2
                session.Store(supplier);
                session.Store(category);
                #endregion

                #region Step_3
                product.Supplier = supplier.Id;
                product.Category = category.Id;
                session.Store(product);
                #endregion

                #region Step_4
                session.SaveChanges();
                #endregion
            }
            #endregion

            return Ok($"Document {product.Id} was created successfully");
        }

        public class RunParams
        {
            public string SupplierName { get; set; }
            public string SupplierPhone { get; set; }
            public string ProductName { get; set; }
        }
    }
}
