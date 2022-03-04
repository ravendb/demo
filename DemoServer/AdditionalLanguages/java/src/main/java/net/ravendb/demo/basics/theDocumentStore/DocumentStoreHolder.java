package net.ravendb.demo.basics.theDocumentStore;
//region Usings
import net.ravendb.client.documents.DocumentStore;
//endregion

//region Demo
//region Step_1
public class DocumentStoreHolder {
//endregion
    //region Step_2
    private static DocumentStore store;
    //endregion

    //region Step_3
    private static DocumentStore createDocumentStore() {
    //endregion
        //region Step_4
        String serverURL = "http://localhost:8080";
        String databaseName = "YourDatabaseName";

        DocumentStore documentStore = new DocumentStore(new String[] { serverURL }, databaseName);
        //endregion

        //region Step_5
        documentStore.initialize();
        //endregion
        return documentStore;
    }

    //region Step_6
    public static DocumentStore getStore() {
        if (store == null) {
            store = createDocumentStore();
        }

        return store;
    }
    //endregion
}
//endregion
