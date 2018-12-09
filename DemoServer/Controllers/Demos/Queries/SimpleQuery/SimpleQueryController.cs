using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;

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
            
            // Query Demo - TO BE IMPLEMENTED

            #endregion region
            
            return Ok("Query Demo - TO BE IMPLEMENTED");
        }
    }
}
