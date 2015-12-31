using System;
using System.Net.Http;
using System.Web.Http;
using DemoMethods.Helpers;
using Raven.Client.Connection;

namespace DemoMethods
{
    public partial class MenuController : DemoApiController
    {
        [HttpGet]
        [Demo("Deploy Northwind", DemoOutputType.String)]
        public object DeployNorthwind()
        {
            try
            {
                DocumentStoreHolder.Store
                    .DatabaseCommands
                    .GlobalAdmin
                    .EnsureDatabaseExists(DocumentStoreHolder.DatabaseName);

                var url = string.Format("{0}/studio-tasks/createSampleData", DocumentStoreHolder.Store.Url.ForDatabase(DocumentStoreHolder.DatabaseName));
                var requestFactory = DocumentStoreHolder.Store.JsonRequestFactory;

                var request = requestFactory.CreateHttpJsonRequest(new CreateHttpJsonRequestParams(null, url, "POST", DocumentStoreHolder.Store.DatabaseCommands.PrimaryCredentials, DocumentStoreHolder.Store.Conventions));
                request.ExecuteRequest();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return string.Format("Northwind was deployed to '{0}' database.", DocumentStoreHolder.DatabaseName);
        }
    }
}