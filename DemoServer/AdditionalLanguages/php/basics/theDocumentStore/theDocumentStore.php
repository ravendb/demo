<?php

namespace RavenDB\Demo\basics\theDocumentStore;

//region Usings
use RavenDB\Documents\DocumentStore;
//endregion


//region Demo
//region Step_1
class DocumentStoreHolder
//endregion
{
    //region Step_2
    private static ?DocumentStore $store = null;
    //endregion

    //region Step_3
    private static function createDocumentStore(): DocumentStore {
    //endregion
        //region Step_4
        $serverURL = "http://localhost:8080";
        $databaseName = "YourDatabaseName";

        $documentStore = new DocumentStore($serverURL, $databaseName);
        //endregion

        //region Step_5
        $documentStore->initialize();
        //endregion
        return $documentStore;
    }

    //region Step_6
    public static function getStore(): DocumentStore {
        if (self::$store == null) {
            $store = self::createDocumentStore();
        }

        return self::$store;
    }
    //endregion
}
//endregion
