package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    loadRelatedDocuments(5.2, "050-123456")
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

type Product struct {
    Name string
    Supplier string
    Category string
    PricePerUnit float64
}

type Supplier struct {
    ID string
    Name string
    Phone string
}

//region Demo
func loadRelatedDocuments(pricePerUnit float64, phone string) error {

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()
    
    //region Step_1
    var product *Product
    err = session.Include("supplier").
                  Load(&product, "products/34-A")
    if err != nil {
        return err
    }
    if product == nil {
        return nil
    }
    //endregion

    //region Step_2
    var supplier *Supplier
    err = session.Load(&supplier, product.Supplier)
    if err != nil || supplier == nil {
        return err
    }
    //endregion

    //region Step_3
    product.PricePerUnit = pricePerUnit
    supplier.Phone = phone
    //endregion

    //region Step_4
    err = session.SaveChanges()
    if err != nil {
        return err
    }
    //endregion

    return nil
}
//endregion
