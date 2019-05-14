package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    sortingQueryResults(20)
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
func sortingQueryResults(numberOfUnits int) error {

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_1
    queriedType := reflect.TypeOf(&Product{})
    query := session.QueryCollectionForType(queriedType)
    //endregion

    //region Step_2
    query = query.Where("UnitsInStock", ">", numberOfUnits)
    //endregion
    //region Step_3
    query = query.OrderByDescendingWithOrdering("UnitsInStock", ravendb.OrderingTypeLong)
    //endregion
    //region Step_4
    query = query.OrderByWithOrdering("Name", ravendb.OrderingTypeAlphaNumeric)
    //endregion

    //region Step_5
    var sortedProducts []*Product
    err = query.GetResults(&sortedProducts)
    if err != nil {
        return err
    }
    //endregion

    return nil
}

type Product struct {
    ID              string
    Name            string
    Supplier        string
    Category        string
    QuantityPerUnit string
    PricePerUnit    float64
    UnitsInStock    int 
    UnitsOnOrder    int
    Discontinued    bool 
    ReorderLevel    int 
}
//endregion
