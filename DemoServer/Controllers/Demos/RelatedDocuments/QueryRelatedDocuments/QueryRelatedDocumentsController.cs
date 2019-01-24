using DemoCommon.Models;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using DemoServer.Utils.UserId;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Documents.Session;
using Raven.Client.Documents;
#endregion

namespace DemoServer.Controllers.Demos.RelatedDocuments.QueryRelatedDocuments
{
    public class QueryRelatedDocumentsController : DemoCodeController
    {
        public QueryRelatedDocumentsController(UserIdContainer userId, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(userId, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                #region Step_1
                List<Order> shippedOrders = session.Query<Order>()
                    .Include(c => c.Lines.Select(x => x.Product)) 
                    .Where(c => c.ShippedAt != null)
                    .ToList();
                #endregion
                
                #region Step_2
                foreach (Order shippedOrder in shippedOrders)
                {
                    List<string> productIds = shippedOrder.Lines.Select(x => x.Product).ToList();
                #endregion
                    
                    for (var i = 0; i < productIds.Count; i++)
                    {   
                        #region Step_3
                        Product product = session.Load<Product>(productIds[i]);;
                        product.UnitsOnOrder += shippedOrder.Lines[i].Quantity;
                        #endregion
                    }
                }
                
                #region Step_4
                session.SaveChanges();
                #endregion
            }
            #endregion
            
            return Ok($"The Product Documents were updated successfully");
        }
    }
}
