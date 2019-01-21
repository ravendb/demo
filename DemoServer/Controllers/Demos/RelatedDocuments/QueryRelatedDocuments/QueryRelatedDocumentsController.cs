using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
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
        public QueryRelatedDocumentsController(HeadersAccessor headersAccessor, UserStoreCache userStoreCache, MediaStoreCache mediaStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, userStoreCache, mediaStoreCache, databaseSetup)
        {
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
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

        public class RunParams
        {
            public decimal PricePerUnit { get; set; }

            public string Phone { get; set; }
        }
    }
}
