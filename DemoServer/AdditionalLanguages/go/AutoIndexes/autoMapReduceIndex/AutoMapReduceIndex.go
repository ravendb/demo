package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    autoMapReduceIndex()
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
func autoMapReduceIndex() error {

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_1
    queriedType := reflect.TypeOf(&Employee{})
   	query := session.QueryCollectionForType(queriedType)
    //endregion
    
    //region Step_2
    dynamicQuery := query.GroupBy("Address.Country")
    //endregion
    
    //region Step_3
    dynamicQuery = dynamicQuery.SelectKeyWithNameAndProjectedName("Address.Country", "Country")
    query = dynamicQuery.SelectCountWithName("NumberOfEmployees")
    //endregion
    
    //region Step_4
    query = query.OrderByDescending("NumberOfEmployees")
    //endregion
    
    //region Step_5
    var numberOfEmployeesPerCountry []*CountryDetails
        err = query.GetResults(&numberOfEmployeesPerCountry)
        if err != nil {
            return err
        }
    //endregion
    
    return nil
}

type Employee struct {
    ID         string
    LastName   string
    FirstName  string
    Title      string
    Address    *Address 
}
type Address struct {
    Line1      string
    Line2      string
    City       string
    Country    string
}
type CountryDetails struct {
    Country           string
    NumberOfEmployees int
}
//endregion
