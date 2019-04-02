//region Usings
import { 
   "github.com/ravendb/ravendb-go-client"
   "encoding/json"
}  
//endregion
  
//region Demo
func createDocument(companyName, companyPhone, contactName, contactTitle string) error {

    //region Step_1
    newCompany := Company {
        Name:  companyName,
        Phone: companyPhone,
        Contact: &Contact {
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

    //region Step_2
    err = session.Store(newCompany)
    if err != nil {
        return err
    }
    //endregion

    //region Step_3
    theNewDocumentID := newCompany.ID
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
