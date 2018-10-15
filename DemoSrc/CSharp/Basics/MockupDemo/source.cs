// Sample Source file for MockupDemo

var serverUrl = "http://localhost:8080";
var dbName = "Demo";

// Creating a DocumentStore instance
var store = new DocumentStore
{
    Urls = new[] { serverUrl },
    Database = dbName
};

store.Initialize();
