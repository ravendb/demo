package net.ravendb.demo.common;

import net.ravendb.client.documents.DocumentStore;

public class DocumentStoreHolder {

    public static DocumentStore store;

    static {
        store = new DocumentStore("http://live-test.ravendb.net", "DemoUser-74834049-1170-4a02-9108-28e8f4d5165b");
        store.initialize();
    }

}
