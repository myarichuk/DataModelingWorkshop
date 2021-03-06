﻿using System;
using Raven.Client.Documents;

namespace ImportCsvData.Utils
{
    public class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> _store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store => _store.Value;

        private static IDocumentStore CreateStore()
        {
            var store = new DocumentStore
            {
                Urls = Configuration.Settings.Urls,
                Database = Configuration.Settings.OpenBeerDB
            };            

            store.Initialize();
            AppDomain.CurrentDomain.DomainUnload += (_, __) => store.Dispose();

            return store;
        }
    }
}
