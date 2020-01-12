package net.ravendb.demo.basics.theDocumentStore;
//region Usings
import net.ravendb.client.documents.IDocumentStore;
import net.ravendb.client.documents.DocumentStore;
package com.journaldev.singleton;
//endregion

//region Demo
//region Step_1
public class DocumentStoreHolder {
//endregion
    //region Step_2
    private static IDocumentStore store;
    //endregion

    //region Step_3
    private IDocumentStore createDocumentStore()
    //endregion
        //region Step_4
        string serverURL = "http://localhost:8080";
        string databaseName = "YourDatabaseName";

        IDocumentStore documentStore = new DocumentStore
        {
            Urls = new[] { serverURL },
            Database = databaseName
        };
        //endregion

        //region Step_5
        documentStore.initialize();
        //endregion
        return documentStore;
    }

    //region Step_6
    public static IDocumentStore getStore() {
        if(store == null) {
            store = new createDocumentStore();
        }

        return store;
    }
    //endregion
}
//endregion
