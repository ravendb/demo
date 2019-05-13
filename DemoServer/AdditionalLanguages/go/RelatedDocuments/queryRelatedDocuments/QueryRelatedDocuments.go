package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    queryRelatedDocuments()
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
func queryRelatedDocuments() error {

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_1
    queriedType := reflect.TypeOf(&Order{})
    q := session.QueryCollectionForType(queriedType)
    q = q.Include("Lines.Product")
    q = q.WhereNotEquals("ShippedAt", nil)
    
    var shippedOrders []*Order
    err = q.GetResults(&shippedOrders)
    if err != nil {
        return err
    }
    //endregion

    //region Step_2
    for _, shippedOrder := range shippedOrders {
        var productIDs []string
        for _, line := range shippedOrder.Lines {
            productIDs = append(productIDs, line.Product)
        }
    //endregion
        for i, productID := range productIDs {
            //region Step_3 
            var product *Product
            err = session.Load(&product, productID)
            if err != nil {
                return err
            }
            product.UnitsOnOrder += shippedOrder.Lines[i].Quantity
            //endregion
        }
    }

    //region Step_4
    err = session.SaveChanges()
    if err != nil {
        return err
    }
    //endregion

    return nil
}

type Order struct {
    ID        string
    Company   string
    ShippedAt *ravendb.Time
    Lines     []*OrderLine
}
type OrderLine struct {
    Product      string
    ProductName  string
    Quantity     int
}
type Product struct {
    ID   string
    Name string
    Supplier string
    Category string
    UnitsOnOrder int
}
//endregion
