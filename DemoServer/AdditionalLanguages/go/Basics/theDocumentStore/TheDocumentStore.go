package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

func main() {
    createDocumentStore()
}

var globalDocumentStore *ravendb.DocumentStore

//region Demo
//region Step_1
var globalDocumentStore *ravendb.DocumentStore
//endregion

//region Step_2
func createDocumentStore() (*ravendb.DocumentStore, error) {
//endregion    
  
    if globalDocumentStore != nil {
        return globalDocumentStore, nil
    }

    //region Step_3 
    serverURL := "http://localhost:8080"
    databaseName := "YourDatabaseName"
    
    urls := []string {serverURL}
    store := ravendb.NewDocumentStore(urls, databaseName)
    //endregion

    //region Step_4
    store.Certificate = ... 
    store.TrustStore = ...
    //endregion
    
    //region Step_5
    conventions := ravendb.NewDocumentConventions() 
    // Modify conventions as needed
    store.SetConventions(conventions) 
    //endregion

    //region Step_6
    err := store.Initialize()
    if err != nil {
        return nil, err
    }
    //endregion

    //region Step_7
    globalDocumentStore = store
    //endregion
    
    return globalDocumentStore, nil
}
//endregion
