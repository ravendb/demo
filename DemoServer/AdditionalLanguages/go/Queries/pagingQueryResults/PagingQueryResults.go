package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    pagingQueryResults(10, 5)
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
func pagingQueryResults(resultsToSkip int, resultsToTake int) error {

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_1
    queriedType := reflect.TypeOf(&Company{})
    query := session.QueryCollectionForType(queriedType)
    //endregion

    //region Step_2
    var stats *ravendb.QueryStatistics
    query = query.Statistics(&stats)
    //endregion
    
    //region Step_3
    query = query.Skip(resultsToSkip)
    //endregion    
    //region Step_4
    query = query.Take(resultsToTake)
    //endregion

    //region Step_5
    var pagedResults []*Company
    err = query.GetResults(&pagedResults)
    if err != nil {
        return err
    }
    //endregion
    
    //region Step_6 
    totalResults := stats.TotalResults
    //endregion    
    return nil
}

type Company struct {
    ID         string
    ExternalID string 
    Name       string
    Phone      string
}
//endregion
