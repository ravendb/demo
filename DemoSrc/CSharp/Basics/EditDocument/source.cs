var documentID = "companies/1-A";

using (var session = documentStore.OpenSession())
{
    // Load the document 
    var company = session.Load<Company>(documentID);
    
    // Update the data 
    company.Name = "New Company Name";
    
    // Save the entity as a document in the database
    session.SaveChanges();
}
