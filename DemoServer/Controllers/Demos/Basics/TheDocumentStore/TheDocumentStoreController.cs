using System;
using DemoServer.Utils;
using DemoServer.Utils.Cache;
using DemoServer.Utils.Database;
using Raven.Client.Documents;

#region Demo
#region Step_1
public class DocumentStoreHolder
#endregion
{
    #region Step_2
    private static readonly Lazy<IDocumentStore> _store = new Lazy<IDocumentStore>(CreateDocumentStore);
    #endregion

    #region Step_3
    private static IDocumentStore CreateDocumentStore()
    #endregion
    {
        #region Step_4
        var serverURL = "http://localhost:8080";
        var databaseName = "YourDatabaseName";

        var documentStore = new DocumentStore
        {
            Urls = new[] {serverURL},
            Database = databaseName
        };
        #endregion

        #region Step_5
        documentStore.Initialize();
        #endregion
        return documentStore;
    }

    #region Step_6
    public static IDocumentStore Store
    {
        get { return _store.Value; }
    }
    #endregion
}
#endregion
