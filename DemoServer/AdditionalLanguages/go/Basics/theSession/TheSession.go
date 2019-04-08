package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

//region Demo
var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    workWithSession()
    globalDocumentStore.Close()
}

func workWithSession() error {
    //region Step_1
    session, err := globalDocumentStore.OpenSession("YourDatabaseName")
        if err != nil {
        return err
    }   
    //endregion
    
    //region Step_2
    //   Run your business logic:
    //  
    //   Store documents
    //   Load and Modify documents
    //   Query indexes & collections
    //   Delete documents
    //   .... etc.
    //endregion

    //region Step_3
    err = session.SaveChanges()
    if err != nil {
        return err
    }
    //endregion

    //region Step_4
    session.Close()
    //endregion

    return nil
}
//endregion
