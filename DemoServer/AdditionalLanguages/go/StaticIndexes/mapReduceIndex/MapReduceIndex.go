package main

//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion

var globalDocumentStore *ravendb.DocumentStore

func main() {
    createDocumentStore()
    createDatabase()
    mapReduceIndex("USA")
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
func mapReduceIndex(country string) error {

        //region Step_1
        indexName := "Employees/ByCountry"
        index := ravendb.NewIndexCreationTask(indexName)
        //endregion
        
        //region Step_2
        index.Map := `
              map('employees', function(e) {
                 return {
                     Country: e.Address.Country,
                     Count: 1
                 }
             })
        `
        //endregion
        //region Step_3
        index.Reduce := `
              groupBy(x => x.Country)
              .aggregate(g => {
                     return {
                         Country: g.key,
                         Count: g.values.reduce((count, val) => val.Count + count, 0)
                     }
              })
        `
        //endregion
        
        //region Step_4
        err := index.Execute(globalDocumentStore, nil, "")
        if err != nil {
            return err
        }
        //endregion
        
        session, err := globalDocumentStore.OpenSession("")
        if err != nil {
            return err
        }
        defer session.Close()
    
        //region Step_5
        query := session.QueryIndex(indexName)
        query = query.Where("Country", "==", country)
    
        var queryResult *struct {
            Country string
            Count   int
        }
    
        err = query.First(&queryResult)
        if err != nil {
            return err
        }
        //endregion
        
        return nil
    }
    
type Employee struct {
    ID          string
    LastName    string
    FirstName   string
    Title       string
    Address     *Address
    HiredAt     ravendb.Time
    HomePhone   string
    Extension   string
    ReportsTo   string
    Notes       []string
    Territories []string
}
type Address struct {
    Line1      string
    Line2      string
    City       string   
    Country    string
}    
//endregion
