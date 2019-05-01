package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    editDocument("newCompanyName")
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
func editDocument(companyName string) error {

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_1
    var company *Company  
    err = session.Load(&company, "companies/5-A")
    if err != nil {
        return err
    }   
    
    if company == nil {
        return nil
    }
    //endregion
    
    //region Step_2
    company.Name = companyName
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
