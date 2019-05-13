package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    indexRelatedDocuments("Produce")
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
func indexRelatedDocuments(categoryName string) error {
    
    //region Step_1
    index := ravendb.NewIndexCreationTask("Products/ByCategoryName")
    //endregion
    
    //region Step_2
    index.Map = `docs.Products.Select(product => new {
        CategoryName = (this.LoadDocument(product.Category, "Categories")).Name
    })`
    //endregion
    
    //region Step_3
    err := globalDocumentStore.ExecuteIndex(index, "")
    if err != nil {
        return err
    }
    //endregion
    
    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_4  
    q := session.QueryIndex(index.IndexName)
    q = q.WhereEquals("CategoryName", categoryName)
    
    var productsWithCategoryName []*Product
    err = q.GetResults(&productsWithCategoryName)
    //endregion
    
    if err != nil {
        return err
    }
    return nil
}

type Product struct {
    ID   string
    Name string
    Supplier string
    Category string
}
//endregion
