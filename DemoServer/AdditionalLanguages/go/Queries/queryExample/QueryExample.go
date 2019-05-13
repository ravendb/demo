package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    queryExample()
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
func queryExample() error {

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
    {
        query = query.OpenSubclause()
        query = query.WhereEquals("FirstName", "Steven")
        query = query.OrElse()
        query = query.WhereEquals("Title", "Sales Representative")
        query = query.CloseSubclause()
    }
    //endregion
    
    //region Step_3
    query = query.Include("ReportsTo")
    //endregion

    //region Step_4
    var statistics *ravendb.QueryStatistics
    query = query.Statistics(&statistics)
    //endregion

    //region Step_5
    query = query.OrderByDescending("HiredAt")
    //endregion

    //region Step_6    
    projectedType := reflect.TypeOf(&EmployeeDetails{})
    fields := []string{
        "FirstName",
        "LastName",
        "Title",
        "HiredAt",
    }
    query = query.SelectFields(projectedType, fields...)
    //endregion
    
    //region Step_7
    query = query.Take(5)
    //endregion
    
    //region Step_8
    var queryResults []*EmployeeDetails
    err = query.GetResults(&queryResults)
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
    Birthday   ravendb.Time
    HomePhone  string
    Extension  string
    HiredAt    ravendb.Time
    ReportsTo  string
}
type EmployeeDetails struct {
    FirstName string 
    LastName  string
    Title     string
    HiredAt   ravendb.Time
}
//endregion
