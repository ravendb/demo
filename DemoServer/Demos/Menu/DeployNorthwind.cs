using System;
using System.Net.Http;
using DemoServer.Controllers;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                if (deleteDatabase)
                {
                    DocumentStoreHolder.Store
                        .Admin
                        .Server
                        .Send(new DeleteDatabaseOperation(DocumentStoreHolder.NorthwindDatabaseName, hardDelete: true));
                }

                DocumentStoreHolder.Store
                    .Admin
                    .Server
                    .Send(new CreateDatabaseOperation(new DatabaseDocument
                    {
                        Id = DocumentStoreHolder.NorthwindDatabaseName
                    }));

                var requestExecuter = DocumentStoreHolder.Store.GetRequestExecuter();
                JsonOperationContext context;
                using (requestExecuter.ContextPool.AllocateOperationContext(out context))
                {
                    var command = new CreateSampleDataCommand();
                    requestExecuter.Execute(command, context);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return string.Format("Northwind was deployed to '{0}' database.", DocumentStoreHolder.NorthwindDatabaseName);
        }

        private class CreateSampleDataCommand : RavenCommand<object>
        {
            public override HttpRequestMessage CreateRequest(ServerNode node, out string url)
            {
                url = $"{node.Url}/databases/{node.Database}/studio/sample-data";

                return new HttpRequestMessage
                {
                    Method = HttpMethod.Post
                };
            }

            public override void SetResponse(BlittableJsonReaderObject response, bool fromCache)
            {
            }

            public override bool IsReadRequest => false;
        }
    }
}