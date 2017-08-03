using System;
using System.Net.Http;
using DemoServer.Controllers;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Operations;
using Raven.Client.Http;
using Raven.Client.Server;
using Raven.Client.Server.Operations;
using Sparrow.Json;

namespace DemoServer.Demos.Menu
{
    public partial class MenuController : BaseController
    {
        [HttpGet]
        [Route("/menu/deployNorthwind")]
        [Demo("Deploy Northwind", DemoOutputType.String, demoOrder: 305)]
        public object DeployNorthwind(bool deleteDatabase = false)
        {
            if (deleteDatabase)
            {
                DocumentStoreHolder.Store
                    .Admin
                    .Server
                    .Send(new DeleteDatabaseOperation(DocumentStoreHolder.NorthwindDatabaseName, hardDelete: true));

                WaitForDeleteToComplete(DocumentStoreHolder.Store, DocumentStoreHolder.NorthwindDatabaseName);

                DocumentStoreHolder.Store
                    .Admin
                    .Server
                    .Send(new CreateDatabaseOperation(new DatabaseRecord(DocumentStoreHolder.NorthwindDatabaseName)));

                WaitForOperationToComplete(DocumentStoreHolder.Store, DocumentStoreHolder.NorthwindDatabaseName);

            }

            DocumentStoreHolder.Store
              .Admin
                .Send(new CreateSampleDataOperation());

            return string.Format("Northwind was deployed to '{0}' database.", DocumentStoreHolder.NorthwindDatabaseName);
        }

        public class CreateSampleDataOperation : IAdminOperation
        {
            public RavenCommand GetCommand(DocumentConventions conventions, JsonOperationContext context)
            {
                return new CreateSampleDataCommand();
            }

            private class CreateSampleDataCommand : RavenCommand
            {
                public override bool IsReadRequest => false;
                public override HttpRequestMessage CreateRequest(ServerNode node, out string url)
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