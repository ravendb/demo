//region Usings
const { DocumentStore } = require('ravendb');
//endregion

//region Demo
//region Step_1
const serverUrl = 'http://localhost:8080';
const databaseName = 'YourDatabaseName';

const store = new DocumentStore(serverUrl, databaseName);
//endregion

//region Step_2
store.initialize();
//endregion

//region Step_3
module.exports = { store };
//endregion
//endregion
