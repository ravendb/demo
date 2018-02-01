using System.Net.Http;
using DemoServer.Controllers;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Operations;
using Raven.Client.Http;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using Sparrow.Json;

namespace DemoServer.Demos.Menu
{
    public partial class MenuController : BaseController
    {
        [HttpGet]
        [Route("/menu/deployNorthwind")]
        [Demo("Deploy Northwind", DemoOutputType.String, demoOrder: 300)]
        public object DeployNorthwind(bool deleteDatabase = true)
        {
            if (deleteDatabase)
            {
                DocumentStoreHolder.Store
                    .Maintenance
                    .Server
                    .Send(new DeleteDatabasesOperation(DocumentStoreHolder.NorthwindDatabaseName, hardDelete: true));

                WaitForDeleteToComplete(DocumentStoreHolder.Store, DocumentStoreHolder.NorthwindDatabaseName);
            }

            DocumentStoreHolder.Store
                .Maintenance
                .Server
                .Send(new CreateDatabaseOperation(new DatabaseRecord(DocumentStoreHolder.NorthwindDatabaseName)));

            WaitForOperationToComplete(DocumentStoreHolder.Store, DocumentStoreHolder.NorthwindDatabaseName);


            DocumentStoreHolder.Store
                .Maintenance
                .Send(new CreateSampleDataOperation());

            return string.Format("Northwind was deployed to '{0}' database.", DocumentStoreHolder.NorthwindDatabaseName);
        }

        public class CreateSampleDataOperation : IMaintenanceOperation
        {
            public RavenCommand GetCommand(DocumentConventions conventions, JsonOperationContext context)
            {
                return new CreateSampleDataCommand();
            }

            private class CreateSampleDataCommand : RavenCommand
            {
                public override bool IsReadRequest => false;

                public override HttpRequestMessage CreateRequest(JsonOperationContext ctx, ServerNode node, out string url)
                {
                    url = $"{node.Url}/databases/{node.Database}/studio/sample-data";

                    return new HttpRequestMessage
                    {
                        Method = HttpMethod.Post
                    };
                }
            }
        }
    }
}