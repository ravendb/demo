package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    storeAttachement("products/34-A","C:\temp\temp.jpg")
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
func storeAttachement(documentID string, attachmentPath string) error {

    //region Step_1
    stream, err := os.Open(attachmentPath)
    if err != nil {
        return err
    }
    defer stream.Close()
    //endregion
 
    contentType := mime.TypeByExtension(filepath.Ext(attachmentPath))
    attachmentName := filepath.Base(attachmentPath)
    

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_2
    err = session.Advanced().Attachments().StoreByID(documentID, attachmentName, stream, contentType)
    if err != nil {
        return err
    }
    //endregion
 
    //region Step_3
    err = session.SaveChanges()
    if err != nil {
        return err
    }
    //endregion
    
    return nil
}
//endregion
