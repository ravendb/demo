// Define the object to be stored 
var newCompany = new Company { Name = "Hibernating Rhinos", 
                               Phone = "(+972)052-5933777", 
                               Contact = new Contact() { Name = "Ayende", 
                                                         Title = "CEO"} };
           
using (var session = documentStore.OpenSession())
{
    // Store the new company entity in the session
    session.Store(newCompany);
    
    
    // Save the entity as a document in the database
    session.SaveChanges();
}
