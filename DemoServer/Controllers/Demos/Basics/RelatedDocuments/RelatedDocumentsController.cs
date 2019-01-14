using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.Basics.RelatedDocuments
{
    public class RelatedDocumentsController : DemoCodeController
    {
        public RelatedDocumentsController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string supplierName = runParams.SupplierName;
            string supplierPhone = runParams.SupplierPhone;
            string productName = runParams.ProductName;

            #region Demo 
            #region Step_1
            Supplier supplier = new Supplier
            {
                Name = supplierName,
                Phone = supplierPhone
            };

            Category category = new Category
            {
                Name = "NoSQL Databases",
                Description = "Non-relational databases"
            };
            #endregion
            
            #region Step_2
            Product product = new Product
            {
                Name = productName
            };
            #endregion

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_3
                session.Store(supplier);
                session.Store(category);
                #endregion

                #region Step_4
                product.Supplier = supplier.Id;
                product.Category = category.Id;
                #endregion

                #region Step_5
                session.Store(product);
                #endregion

                #region Step_6
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
