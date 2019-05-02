package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    getRevisions()
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

type Company struct {
    ID      string
    Name    string
    Phone   string
    Contact *Contact
}

type Contact struct {
    Name  string
    Title string
}

//region Demo
func getRevisions() error {

    //region Step_1
    myRevisionsConfiguration := &ravendb.RevisionsConfiguration {
        DefaultConfig: &ravendb.RevisionsCollectionConfiguration {
            Disabled: false,
        },
    }

    revisionsConfigurationOperation := ravendb.NewConfigureRevisionsOperation(myRevisionsConfiguration)
    err := globalDocumentStore.Maintenance().Send(revisionsConfigurationOperation)
    if err != nil {
        return nil
    }
    //endregion
    
    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()
    
    //region Step_2
    var company *Company
    err = session.Load(&company, "companies/7-A")
    if err != nil {
        return err
    }
    
    company.Name = "Name 1"
    err = session.SaveChanges()
    if err != nil {
        return err
    }
    
    company.Name = "Name 2"
    company.Phone = "052-1234-567"
    err = session.SaveChanges()
    if err != nil {
        return err
    }
    //endregion
    
    //region Step_3
    var revisions []*Company
    err = session.Advanced().Revisions().GetFor(&revisions, "companies/7-A")
    if err != nil {
        return err
    }
    //endregion
    
    return nil
}
//endregion
