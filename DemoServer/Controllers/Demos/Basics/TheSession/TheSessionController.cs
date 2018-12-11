using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

namespace DemoServer.Controllers.Demos.Basics.TheSession
{
    public class TheSessionController : DemoCodeController
    {
        public TheSessionController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseAccessor databaseAccessor) : base(headersAccessor, documentStoreCache, databaseAccessor)
        {
        }

        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            
            #region Step_1
            using (var session = DocumentStoreHolder.Store.OpenSession())
            #endregion
            {
                #region Step_2
                //   Run your business logic:
                //   
                //   Store documents
                //   Load and Modify documents
                //   Query indexes & collections 
                //   Delete documents
                //   .... etc.
                #endregion
                
                #region Step_3
                session.SaveChanges();
                #endregion
            }
            #endregion

            return Ok("OK"); 
        }
    }
}
