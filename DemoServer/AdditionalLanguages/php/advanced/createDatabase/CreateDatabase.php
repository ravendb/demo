<?php

namespace RavenDB\Demo\advanced\createDatabase;

//region Usings
use RavenDB\Exceptions\ConcurrencyException;
use RavenDB\ServerWide\DatabaseRecord;
use RavenDB\ServerWide\Operations\CreateDatabaseOperation;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

class CreateDatabase
{
    public function __invoke()
    {
        $databaseName = "someDb";

        //region Demo
        try {
            //region Step_1
            $databaseRecord = new DatabaseRecord($databaseName);
            $createDatabaseOperation = new CreateDatabaseOperation($databaseRecord);
            //endregion

            //region Step_2
            DocumentStoreHolder::getStore()->maintenance()->server()->send($createDatabaseOperation);
            //endregion
            
        } catch (ConcurrencyException $e) {
            // Database already exists
        }
        //endregion
    }
}
