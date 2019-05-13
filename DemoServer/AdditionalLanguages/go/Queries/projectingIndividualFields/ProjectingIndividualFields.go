package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    projectingIndividualFieldsInQuery()
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
func projectingIndividualFieldsInQuery() error {

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_1
    queriedType := reflect.TypeOf(&Company{})
    projectedQuery := session.QueryCollectionForType(queriedType)
    //endregion
    
    //region Step_2
    projectedType := reflect.TypeOf(&CompanyDetails{})
    
    fields := []string{"Name", "Address.City", "Address.Country"}
    projections := []string{"CompanyName", "City", "Country"}
    
    queryData := &ravendb.QueryData{
        Fields:      fields,
        Projections: projections,
    }    
    projectedQuery = projectedQuery.SelectFieldsWithQueryData(projectedType, queryData)
    //endregion
    
    //region Step_3
    var projectedResults []*CompanyDetails
    err = projectedQuery.GetResults(&projectedResults)
    if err != nil {
        return err
    }
    //endregion
    
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
type CompanyDetails struct {
    CompanyName string
    City        string
    Country     string
}
//endregion
