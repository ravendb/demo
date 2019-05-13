package main

//region Usings
import (
    "fmt"
    "github.com/ravendb/ravendb-go-client"
)
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    createDocument("aaa","bbb","ccc","ddd")
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
func createDocument(companyName, companyPhone, contactName, contactTitle string) error {

    //region Step_1
    newCompany := Company {
        Name:    companyName,
        Phone:   companyPhone,
        Contact: &Contact {
            Name:  contactName,
            Title: contactTitle,
        },
    }
    //endregion

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_2
    err = session.Store(&newCompany)
    if err != nil {
        return err
    }
    //endregion

    //region Step_3
    theNewDocumentID := newCompany.ID
    //endregion

    //region Step_4
    err = session.SaveChanges()
    if err != nil {
        return err
    }
    //endregion

    fmt.Printf("Created a new document with id: %s \n", theNewDocumentID)
    return nil   
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
//endregion
