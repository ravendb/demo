package net.ravendb.demo.common;

import net.ravendb.client.documents.DocumentStore;
import net.ravendb.client.extensions.JsonExtensions;

public class DocumentStoreHolder {

    public static DocumentStore store;

    public static DocumentStore mediaStore;

    static {
        // replace params below with whatever you run with...
        store = new DocumentStore("http://localhost:8080", "q1");

        // Uncomment the below if you want PascalCase documents properties to be returned in the resulting query documents.
        // store.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());

        store.initialize();

        mediaStore = new DocumentStore("http://localhost:8080", "Media-6d9c0b0c-7142-4b24-b09d-efd85ebd70a8");
        mediaStore.initialize();
    }
}
