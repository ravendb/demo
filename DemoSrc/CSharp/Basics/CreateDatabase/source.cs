try
{
    var databaseName = "Demo";
    documentStore.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(databaseName)));
}
catch (ConcurrencyException e)
{
    // Database already exists
}
