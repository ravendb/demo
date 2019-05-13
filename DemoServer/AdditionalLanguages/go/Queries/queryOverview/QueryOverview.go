package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    queryOverview()
    globalDocumentStore.Close()
}

func createDocumentStore() (*ravendb.DocumentStore, error) {
    if globalDocumentStore != nil {
        return globalDocumentStore, nil
    }
    urls := []string{"http://localhost:8080"}
    store := ravendb.NewDocumentStore(urls, "testGO")
    err := store.Initialize()
    if err != nil {
        return nil, err
    }
    globalDocumentStore = store
    return globalDocumentStore, nil
}

func createDatabase() {
    databaseRecord := ravendb.NewDatabaseRecord()
    databaseRecord.DatabaseName = "testGO"
    createDatabaseOperation := ravendb.NewCreateDatabaseOperation(databaseRecord, 1)
    var err = globalDocumentStore.Maintenance().Server().Send(createDatabaseOperation)
    if err != nil {
        fmt.Printf("d.store.Maintenance().Server().Send(createDatabaseOperation) failed with %s\n", err)
    }
}

//region Demo
func queryOverview() error {

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()
    
    //region Step_1
    queriedType := reflect.TypeOf(&Employee{})
    queryDefinition := session.QueryCollectionForType(queriedType)
    //endregion
    
    //region Step_2
    // Define actions such as:
    
    // Filter documents by documents fields
    // Filter documents by text criteria
    // Include related documents
    // Get the query stats
    // Sort results
    // Customise the returned entity fields (Projections)
    // Control results paging
    //endregion
    
    //region Step_3
    var queryResults []*Employee
    err = queryDefinition.GetResults(&queryResults)
    if err != nil {
        return err
    }
    //endregion
    
    return nil
}

type Employee struct {
    ID         string
    LastName   string
    FirstName  string
    Title      string
}
//endregion
