using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents.Session;
#endregion

namespace DemoServer.Controllers.Demos.RelatedDocuments.CreateRelatedDocuments
{
    public class CreateRelatedDocumentsController : DemoCodeController
    {
        public CreateRelatedDocumentsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            string supplierName = runParams.SupplierName?? "Hibernating Rhinos";
            string supplierPhone = runParams.SupplierPhone?? "(+972)52-5486969";
            string productName = runParams.ProductName?? "RavenDB";

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
