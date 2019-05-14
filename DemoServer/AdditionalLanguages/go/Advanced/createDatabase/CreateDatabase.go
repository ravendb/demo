package main

//region Usings
import {
    "fmt" 
    "github.com/ravendb/ravendb-go-client"
}
//endregion

func main() {
    createDatabase()
}

//region Demo
func createDatabase() error {

    //region Step_1
    databaseRecord := ravendb.NewDatabaseRecord()
    databaseRecord.DatabaseName = "NameOfDatabase"
    
    createDatabaseOperation := ravendb.NewCreateDatabaseOperation(databaseRecord, 1)
    //endregion
    
    //region Step_2
    err := globalDocumentStore.Maintenance().Server().Send(createDatabaseOperation)
    if err != nil {
        if _, ok := err.(*ravendb.ConcurrencyError); ok {
            fmt.Printf("Database '%s' already exists\n", databaseRecord.DatabaseName)
            return nil
        }
        fmt.Printf("createDatabaseOperation failed with %s\n", err)
        return err
    }
    //endregion

    return nil
}
//endregion
