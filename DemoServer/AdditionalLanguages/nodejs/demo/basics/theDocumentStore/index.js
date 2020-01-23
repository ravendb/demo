//region Usings
const { DocumentStore } = require('ravendb');
//endregion

//region Demo
//region Step_1
const serverUrl = 'http://localhost:8080';
const databaseName = 'YourDatabaseName';

const documentStore = new DocumentStore([serverUrl], databaseName);
//endregion

//region Step_2
documentStore.initialize();
//endregion

//region Step_3
module.exports = { documentStore };
//endregion
//endregion
