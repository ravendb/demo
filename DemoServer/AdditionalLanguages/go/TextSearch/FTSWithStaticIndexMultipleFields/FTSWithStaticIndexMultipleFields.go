package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    fullTextSearchMultipleField("Floyd")
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
func fullTextSearchMultipleField(searchTerm string) error {

    //region Step_1
    indexName := "Song/TextData"
    index := ravendb.NewIndexCreationTask(indexName)
    //endregion
 
    //region Step_2
    index.Map = `
        from song in docs.LastFms
        select new {
             SongData = new {
                song.Artist,
                song.Title,
                song.Tags,
                song.TrackId
             }
        }`
    //endregion
    
    //region Step_3    
    index.Index("SongData", ravendb.FieldIndexingSearch)
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
    query := session.QueryIndex(indexName)
    query = query.Search("SongData", searchTerm)
    query = query.Take(20)
 
    var results []*LastFm
    err = query.GetResults(&results)
    if err != nil {
        return err
    }
    //endregion
    
    return nil
 }

type LastFm struct {
    ID        string
    Artist    string
    Title     string
    TrackID   string
    Tags      []string
}
//endregion
