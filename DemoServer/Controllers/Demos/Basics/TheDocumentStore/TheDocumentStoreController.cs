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
        string serverURL = "http://localhost:8080";
        string databaseName = "YourDatabaseName";

        IDocumentStore documentStore = new DocumentStore
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
