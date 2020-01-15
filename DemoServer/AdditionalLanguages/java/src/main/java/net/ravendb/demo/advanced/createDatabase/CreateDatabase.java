package net.ravendb.demo.advanced.createDatabase;

//region Usings
import net.ravendb.client.exceptions.ConcurrencyException;
import net.ravendb.client.serverwide.DatabaseRecord;
import net.ravendb.client.serverwide.operations.CreateDatabaseOperation;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;

public class CreateDatabase {

    public void run() {
        String databaseName = "someDb";

        //region Demo
        try {
            //region Step_1
            DatabaseRecord databaseRecord = new DatabaseRecord(databaseName);
            CreateDatabaseOperation createDatabaseOperation = new CreateDatabaseOperation(databaseRecord);
            //endregion

            //region Step_2
            DocumentStoreHolder.store.maintenance().server().send(createDatabaseOperation);
            //endregion
        } catch (ConcurrencyException e) {
            // Database already exists
        }
        //endregion
    }

}
