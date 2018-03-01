package net.ravendb.demo;

import net.ravendb.client.documents.DocumentStore;
import net.ravendb.client.documents.IDocumentStore;
import net.ravendb.client.extensions.JsonExtensions;

public class DocumentStoreHolder {

    private static String northwindDatabaseName;
    private static String mediaDatabaseName;
    private static String serverUrl;

    private static IDocumentStore mediaStore;
    private static IDocumentStore northwindStore;

    public static void setDbInfo(String serverUrl, String dbNameNorthwind, String dbNameMedia) {
        DocumentStoreHolder.serverUrl = serverUrl;
        DocumentStoreHolder.northwindDatabaseName = dbNameNorthwind;
        DocumentStoreHolder.mediaDatabaseName = dbNameMedia;
    }

    private static IDocumentStore createNorthwindStore() {
        DocumentStore store = new DocumentStore(serverUrl, northwindDatabaseName);
        store.getConventions().getEntityMapper()
                .setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        store.initialize();

        return store;
    }

    private static IDocumentStore createMediaStore() {
        DocumentStore store = new DocumentStore(serverUrl, mediaDatabaseName);
        store.getConventions().getEntityMapper()
                .setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        store.initialize();
        return store;
    }

    public static String getServerUrl() {
        return serverUrl;
    }

    public static String getNorthwindDatabaseName() {
        return northwindDatabaseName;
    }

    public static String getMediaDatabaseName() {
        return mediaDatabaseName;
    }

    public static IDocumentStore getStore() {
        if (northwindStore == null) {
            synchronized (DocumentStoreHolder.class) {
                if (northwindStore == null) {
                    northwindStore = createNorthwindStore();
                }
            }
        }
        return northwindStore;
    }

    public static IDocumentStore getMediaStore() {
        if (mediaStore == null) {
            synchronized (DocumentStoreHolder.class) {
                if (mediaStore == null) {
                    mediaStore = createMediaStore();
                }
            }
        }
        return mediaStore;
    }

}
