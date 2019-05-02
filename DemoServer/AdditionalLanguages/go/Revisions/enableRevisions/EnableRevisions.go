package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    enableRevisions("collection1","collection2")
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
func enableRevisions(collection1, collection2 string) error {

    //region Step_1
    dur := ravendb.Duration(time.Hour * 24 * 14)
    defaultConfig := &ravendb.RevisionsCollectionConfiguration{
        Disabled:                 false,
        PurgeOnDelete:            false,
        MinimumRevisionsToKeep:   5,
        MinimumRevisionAgeToKeep: &dur,
    }
    //endregion
    
    //region Step_2
    collectionConfig1 := &ravendb.RevisionsCollectionConfiguration{
        Disabled: true,
    }
    collectionConfig2 := &ravendb.RevisionsCollectionConfiguration{
        PurgeOnDelete: true,
    }
    collections := map[string]*ravendb.RevisionsCollectionConfiguration{
        collection1: collectionConfig1,
        collection2: collectionConfig2,
    }
    //endregion

    //region Step_3
    myRevisionsConfiguration := &ravendb.RevisionsConfiguration{
            DefaultConfig: defaultConfig,
            Collections:   collections,
    }
    //endregion 
     
    //region Step_4    
    revisionsConfigurationOperation := ravendb.NewConfigureRevisionsOperation(myRevisionsConfiguration)
    return globalDocumentStore.Maintenance().Send(revisionsConfigurationOperation)
    //endregion
}
//endregion
