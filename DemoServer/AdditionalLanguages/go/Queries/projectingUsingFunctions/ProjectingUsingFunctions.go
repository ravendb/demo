package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    ProjectingUsingFunctions()
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
func ProjectingUsingFunctions() error {

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    
    rawQueryString := 
        //region Step_1
        `declare function output(employee) {
            var formatName  = function(employee) { return "FullName: " + employee.FirstName + " " + employee.LastName; };
            var formatTitle = function(employee) { return "Title: " + employee.Title };
            return { FullName : formatName(employee), Title : formatTitle(employee) };
        }
        //endregion
        //region Step_2 
        from Employees as employee select output(employee)`
        //endregion

    //region Step_3
    projectedQueryWithFunctions := session.RawQuery(rawQueryString)
    //endregion

    //region Step_4
    var projectedResults []*EmployeeDetails
    err = projectedQueryWithFunctions.GetResults(&projectedResults)
    if err != nil {
        return err
    }
    //endregion
    
    return nil
}

type Employee struct {
    ID        string
    FirstName string
    LastName  string
    Title     string 
}
type EmployeeDetails struct {
    FullName  string
    Title     string
}
//endregion
