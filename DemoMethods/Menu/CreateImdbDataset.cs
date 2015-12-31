using System;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using DemoMethods.Entities;
using DemoMethods.Helpers;
using Raven.Abstractions.Data;
using Raven.Client.Connection;
using Raven.Imports.Newtonsoft.Json;
using Raven.Json.Linq;

namespace DemoMethods
{
    public partial class MenuController : DemoApiController
    {
        [HttpGet]
        [Demo("Create IMDb Dataset", DemoOutputType.String, demoOrder: 320)]
        public object CreateImdbDataset(string path = null)
        {
            // path = @"C:\Users\adi\Downloads\lastfm_subset.zip";
            // path = @"C:\Users\adi\Downloads\lastfm_train.zip";
            try
            {
                DocumentStoreHolder.Store
                    .DatabaseCommands
                    .GlobalAdmin
                    .EnsureDatabaseExists(DocumentStoreHolder.DatabaseName);

                var url = string.Format("{0}/studio-tasks/loadCsvFile", DocumentStoreHolder.Store.Url.ForDatabase(DocumentStoreHolder.DatabaseName));

                var fs = File.OpenRead(path);
                Upload(url, fs);

                return $"Northwind was deployed to '{DocumentStoreHolder.DatabaseName}' database.";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private System.IO.Stream Upload(string actionUrl, Stream paramFileStream)
        {
            HttpContent fileStreamContent = new StreamContent(paramFileStream);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent, "file1", "file1");
                var response = client.PostAsync(actionUrl, formData).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return response.Content.ReadAsStreamAsync().Result;
            }
        }
    }
}