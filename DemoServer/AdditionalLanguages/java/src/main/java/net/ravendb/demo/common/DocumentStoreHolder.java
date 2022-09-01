package net.ravendb.demo.common;

import net.ravendb.client.documents.DocumentStore;
import net.ravendb.client.extensions.JsonExtensions;

public class DocumentStoreHolder {

    public static DocumentStore store;

    public static DocumentStore mediaStore;

    static {
        // replace params below with whatever you run with...
        store = new DocumentStore("http://localhost:8080", "q1");

        // Since we target the Sample Data,
        // must use the below to convert between the camelCase Java classes props and the PascalCase json documents.
        store.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());

        store.initialize();

        mediaStore = new DocumentStore("http://localhost:8080", "Media-c6c67f10-bfd8-4575-bac2-9d7f056f0161");
        mediaStore.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        mediaStore.initialize();
    }
}
