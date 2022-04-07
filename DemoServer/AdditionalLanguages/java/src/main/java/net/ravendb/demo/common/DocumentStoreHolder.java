package net.ravendb.demo.common;

import net.ravendb.client.documents.DocumentStore;
import net.ravendb.client.extensions.JsonExtensions;

public class DocumentStoreHolder {

    public static DocumentStore store;

    public static DocumentStore mediaStore;

    static {
        store = new DocumentStore("http://live-test.ravendb.net", "aaa");
        store.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        store.initialize();

        mediaStore = new DocumentStore("http://live-test.ravendb.net", "aaa");
        mediaStore.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        mediaStore.initialize();
    }

}
