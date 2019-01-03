using System.Threading.Tasks;
using DemoCommon.Utils.Database;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Exceptions;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
#endregion

namespace DemoServer.Controllers.Demos.Advanced.CreateDatabase
{
    public class CreateDatabaseController : DemoCodeController
    {
        private readonly DatabaseName _databaseName;

        public CreateDatabaseController(HeadersAccessor headersAccessor, DocumentStoreCache documentStoreCache,
            DatabaseSetup databaseSetup, DatabaseName databaseName) : base(headersAccessor, documentStoreCache, databaseSetup)
        {
            _databaseName = databaseName;
        }

        protected override Task SetDemoPrerequisites()
        {
            return Task.CompletedTask;
            //TODO: Delete the database if exists
        }

        [HttpPost]
        public IActionResult Run()
        {
            var databaseName = _databaseName.For(UserId);

            #region Demo
            
            try
            {
                #region Step_1
                var databaseRecord = new DatabaseRecord(databaseName);
                var createDatabaseOperation = new CreateDatabaseOperation(databaseRecord);
                #endregion
                
                #region Step_2
                DocumentStoreHolder.Store.Maintenance.Server.Send(createDatabaseOperation);
                #endregion
            }
            catch (ConcurrencyException)
            {
                // Database already exists
            }
            #endregion
            
            return Ok($"Database {databaseName} was created successfully");
        }
    }
}
