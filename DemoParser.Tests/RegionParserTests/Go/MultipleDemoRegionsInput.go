//region Usings
import "github.com/ravendb/ravendb-go-client"
//endregion
  
//region Demo
//region Step_1
type Company struct {
    ID      string
    Name    string   `json:"name,omitempty"`
    Phone   string   `json:"phone,omitempty"`
    Contact *Contact `json:"contact"`
}
//endregion

//region Step_2
type Employee struct {
	ID          string
	LastName    string       `json:"LastName"`
	FirstName   string       `json:"FirstName"`
	Title       string       `json:"Title"`
	Address     *Address     `json:"Address"`
	HiredAt     ravendb.Time `json:"HiredAt"`
	Birthday    ravendb.Time `json:"Birthday"`
	HomePhone   string       `json:"HomePhone"`
	Extension   string       `json:"Extension"`
	ReportsTo   string       `json:"ReportsTo"` // id of Employee struct
	Notes       []string     `json:"Notes"`
	Territories []string     `json:"Territories"`
}
//endregion
//endregion

//region Demo
func createDocument(companyName, companyPhone, contactName, contactTitle string) error {

    //region Step_3
    newCompany := &northwind.Company{
        Name:  companyName,
        Phone: companyPhone,
        Contact: &northwind.Contact{
          Name:  contactName,
          Title: contactTitle,
         },
    }
    //endregion

    session, err := globalDocumentStore.OpenSession("")
    if err != nil {
        return err
    }
    defer session.Close()

    //region Step_4
    err = session.Store(newCompany)
    if err != nil {
        return err
    }
    //endregion

    //region Step_5
    theNewDocumentID := newCompany.ID
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


