using System.Threading.Tasks;
using DemoServer.Utils;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents;
using Raven.Client.Exceptions;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
#endregion

namespace DemoServer.Controllers.Demos.Basics.CreateDatabase
{
    public class CreateDatabaseController : DemoCodeController
    {
        public CreateDatabaseController(HeadersAccessor headersAccessor) : base(headersAccessor)
        {
        }

        public override Task SetPrerequisites()
        {
            return Task.CompletedTask;
            //TODO: Delete the database if exists
        }

        [HttpPost]
        public IActionResult Run()
        {
            var serverURL = "http://localhost:8080";
            var databaseName = "DemoExample";
            //TODO: replace databaseName with the user database

            #region Demo
            
            #region Step_1
            // Init the Document Store
            var documentStore = new DocumentStore
            {
                Urls = new[] { serverURL }, // For example: serverUrl = "http://localhost:8080"
                Database = databaseName
            };
            
            documentStore.Initialize();
            #endregion

            #region Step_2
            try
            {
                // Create the new database
                documentStore.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(databaseName)));
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
