//region Usings
const { CreateDatabaseOperation } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');

async function run () {
    const databaseName = 'someDb';

    //region Demo
    try {
        //region Step_1
        const databaseRecord = { databaseName };
        const createDatabaseOperation = new CreateDatabaseOperation(databaseRecord);
        //endregion

        //region Step_2
        await documentStore.maintenance.server.send(createDatabaseOperation);
        //endregion
    } catch (err) {
        if (err.name === 'ConcurrencyException') {
            // Database already exists
        }
    }
    //endregion
}

module.exports = { run };
