package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    createRelatedDocuments("someProductName","someSupplierName","somePhoneNumber")
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
func createRelatedDocuments(productName, supplierName, supplierPhone string) error {

    //region Step_1
    supplier := &Supplier {
        Name:  supplierName,
            Phone: supplierPhone,
    }    
    category := &Category {
        Name:        "NoSQL Databases",
        Description: "Non-relational databases",
    }
    //endregion    
    //region Step_2
    product := &Product{
        Name: productName,
    }    
    //endregion
    
    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_3
    err = session.Store(supplier)
    if err != nil {
        return err
    }    
    err = session.Store(category)
    if err != nil {
        return err
    }
    //endregion

    //region Step_4
    product.Supplier = supplier.ID
    product.Category = category.ID
    //endregion

    //region Step_5
    err = session.Store(product)
    if err != nil {
        return err
    }
    //endregion
     
    //region Step_6
    err = session.SaveChanges()
    if err != nil {
        return err
    }
    //endregion
  
    return nil 
}
//endregion
