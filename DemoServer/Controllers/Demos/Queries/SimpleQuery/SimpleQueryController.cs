using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
#endregion

namespace DemoServer.Controllers.Demos.Queries.SimpleQuery
{
    public class SimpleQueryController : DemoCodeController
    {
        public SimpleQueryController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseAccessor databaseAccessor) : base(headersAccessor, documentStoreCache, databaseAccessor)
        {
        }

        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            
            // TO BE IMPLEMENTED
            // I only added this demo to see how 2 categories will look like in main page

            #endregion region
            
            return Ok("Query OK");
        }

       
    }
}
