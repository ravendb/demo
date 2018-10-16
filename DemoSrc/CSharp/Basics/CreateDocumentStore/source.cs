var serverURL = "http://localhost:8080";
var databaseName = "Demo";

// Init the Document Store
var store = new DocumentStore
{
    Urls = new[] { serverURL },
    Database = databaseName
};
  
store.Initialize();

return store;
