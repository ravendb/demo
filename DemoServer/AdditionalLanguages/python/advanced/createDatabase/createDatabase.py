from database import DocumentStoreHolder
from demo_example import Example

#region Usings
from ravendb import CreateDatabaseOperation
from ravendb.serverwide.database_record import DatabaseRecord
#endregion

class CreateDatabase(Example):
    def run(self, run_params=None):
        database_name = "someDb"

        #region Demo
        try:
            #region Step_1
            database_record = DatabaseRecord(database_name)
            create_database_operation = CreateDatabaseOperation(database_record)
            #endregion

            #region Step_2
            DocumentStoreHolder.store().maintenance.server.send(create_database_operation)
            #endregion

        except Exception as e:
            # Database already exists
            pass
        #endregion
