using DemoCommon.Models;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using System;
using System.Linq;
#endregion


namespace DemoServer.Controllers.Demos.StaticIndexes.MapIndex
{
    public class MapIndexController : DemoCodeController
    {
        public MapIndexController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseSetup databaseSetup) : base(headersAccessor, documentStoreCache, databaseSetup)
        {
        }
        
        [HttpPost]
        public IActionResult Run()
        {
            #region Demo
            
            // TODO...
            
            #endregion 
            
            //TODO: How to show results ?
            return Ok($"Todo.."); 
        }
    }
}
