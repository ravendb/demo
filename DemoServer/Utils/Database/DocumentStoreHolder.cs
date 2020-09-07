﻿using System;
using System.Security.Cryptography.X509Certificates;
using DemoCommon.Models;
using Raven.Client.Documents;

namespace DemoServer.Utils.Database
{
    public class DocumentStoreHolder
    {
        private readonly DatabaseSettings _databaseSettings;

        public DocumentStoreHolder(Settings settings)
        {
            _databaseSettings = settings.Database;
            ValidateSettings();
        }

        private void ValidateSettings()
        {
            if (_databaseSettings.Urls == null || _databaseSettings.Urls.Length == 0)
                throw new InvalidOperationException($"{nameof(_databaseSettings.Urls)} was not provided in the application settings.");

            if (string.IsNullOrEmpty(_databaseSettings.Name))
                throw new InvalidOperationException($"{nameof(_databaseSettings.Name)} was not provided in the application settings.");
        }

        public IDocumentStore CreateStore(string databaseName)
        {
            var store = new DocumentStore
            {
                Urls = _databaseSettings.Urls,
                Database = databaseName
            };

            if (_databaseSettings.CertificatePath != null)
            {
                var certificatePassword = _databaseSettings.CertificatePassword;
                var certificate = new X509Certificate2(_databaseSettings.CertificatePath, certificatePassword);
                store.Certificate = certificate;
                
                store.AfterDispose += (sender, args) => certificate.Dispose();
            }

            if (_databaseSettings.RequestsTimeoutInSec.HasValue)
            {
                store.Conventions.RequestTimeout = TimeSpan.FromSeconds(_databaseSettings.RequestsTimeoutInSec.Value);
            }

            store.OnBeforeQuery += (sender, args) =>
                args.QueryCustomization.WaitForNonStaleResults(TimeSpan.FromSeconds(30));

            return store.Initialize();
        }
    }
}
