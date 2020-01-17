//region Usings
const { DocumentStore } = require('ravendb');
//endregion

// there's less steps here than in java or C# due to singleton nature of node.js module
// not sure how they should be laid out

//region Demo
//region Step_1
//endregion

//region Step_2
const serverUrl = 'http://localhost:8080';
const databaseName = 'YourDatabaseName';
//endregion

//region Step_3
const store = new DocumentStore(serverUrl, databaseName);
//endregion
//region Step_4
//endregion
//region Step_5
store.initialize();
//endregion

//region Step_6
module.exports = { store };
//endregion
//endregion
