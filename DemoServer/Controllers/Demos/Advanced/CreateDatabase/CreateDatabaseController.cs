using System.Threading.Tasks;
using DemoServer.Utils;
using DemoServer.Utils.Database;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents;
using Raven.Client.Exceptions;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
#endregion

namespace DemoServer.Controllers.Demos.Advanced.CreateDatabase
{
    public class CreateDatabaseController : DemoCodeController
    {
        public CreateDatabaseController(HeadersAccessor headersAccessor, DatabaseAccessor databaseAccessor) : base(
            headersAccessor, databaseAccessor)
        {
        }

        protected override Task SetDemoPrerequisites()
        {
            return Task.CompletedTask;
            //TODO: Delete the database if exists
        }

        [HttpPost]
        public IActionResult Run()
        {
            var serverUrl = DatabaseAccessor.GetFirstDatabaseUrl();
            var databaseName = DatabaseAccessor.GetDatabaseName(UserId);

            #region Demo
            
            #region Step_1
            // Init the Document Store
            var documentStore = new DocumentStore
            {
                Urls = new[] { serverUrl }, // For example: serverUrl = "http://localhost:8080"
                Database = databaseName
            };
            
            documentStore.Initialize();
            #endregion

            #region Step_2
            try
            {
                // Create the new database
                var databaseRecord = new DatabaseRecord(databaseName);
                documentStore.Maintenance.Server.Send(new CreateDatabaseOperation(databaseRecord));
            }
            catch (ConcurrencyException)
            {
                // Database already exists
            }
            #endregion
            #endregion
            
            return Ok($"Database {databaseName} was created successfully");
        }
    }
}
