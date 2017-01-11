using System;
using System.Net.Http;
using DemoServer.Controllers;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Abstractions.Data;
using Raven.Client.Connection;

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
                        .DatabaseCommands
                        .GlobalAdmin
                        .DeleteDatabase(DocumentStoreHolder.NorthwindDatabaseName, hardDelete: true);
                }

                DocumentStoreHolder.Store
                    .DatabaseCommands
                    .GlobalAdmin
                    .CreateDatabase(new DatabaseDocument
                    {
                        Id = DocumentStoreHolder.NorthwindDatabaseName,
                        Settings =
                        {
                            {"Raven/DataDir", $"~/{DocumentStoreHolder.NorthwindDatabaseName}" }
                        }
                    });

                var url = string.Format("{0}/studio/sample-data", DocumentStoreHolder.Store.Url.ForDatabase(DocumentStoreHolder.NorthwindDatabaseName));
                var requestFactory = DocumentStoreHolder.Store.JsonRequestFactory;

                var request = requestFactory.CreateHttpJsonRequest(new CreateHttpJsonRequestParams(null, url, HttpMethod.Post, DocumentStoreHolder.Store.DatabaseCommands.PrimaryCredentials, DocumentStoreHolder.Store.Conventions));
                request.ExecuteRequest();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return string.Format("Northwind was deployed to '{0}' database.", DocumentStoreHolder.NorthwindDatabaseName);
        }
    }
}