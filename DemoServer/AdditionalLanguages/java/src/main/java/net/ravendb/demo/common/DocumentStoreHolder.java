package net.ravendb.demo.common;

import net.ravendb.client.documents.DocumentStore;
import net.ravendb.client.extensions.JsonExtensions;

public class DocumentStoreHolder {

    public static DocumentStore store;

    public static DocumentStore mediaStore;

    static {
        store = new DocumentStore("http://live-test.ravendb.net", "DemoUser-74834049-1170-4a02-9108-28e8f4d5165b");
        store.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        store.initialize();

        mediaStore = new DocumentStore("http://live-test.ravendb.net", "DemoUser-74834049-1170-4a02-9108-28e8f4d5165b");
        mediaStore.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        mediaStore.initialize();
    }

}
