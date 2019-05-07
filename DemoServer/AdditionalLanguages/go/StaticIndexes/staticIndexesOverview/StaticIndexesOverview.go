package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    staticIndexesOverview()
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
func staticIndexesOverview() error {

    //region Step_1
    indexName := "Employees/ByLastName"
    index := ravendb.NewIndexCreationTask(indexName)
    //endregion
    
    //region Step_2
    // Define:
    //    Map(s) functions
    //    Reduce function
    //    Additional indexing options per field
    //endregion
    
    //region Step_3
    index.Map = "from e in docs.Employees select new { e.LastName }"
    //endregion

    //region Step_4
    err := index.Execute(globalDocumentStore, nil, "")
    if err != nil {
        return err
    }
    //endregion

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()
    
    //region Step_5
    queryOnIndex := session.QueryIndex(indexName)
    queryOnIndex = queryOnIndex.Where("LastName", "==", "SomeName")
    
    var queryResults []*Employee
    err = queryOnIndex.GetResults(&queryResults)
    if err != nil {
        return err
    }
    //endregion
    
    return nil
}
//endregion
