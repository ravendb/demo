//region Usings
const { CreateDatabaseOperation } = require('ravendb');
//endregion
const { store } = require('../../common/docStoreHolder');

async function run () {
    const databaseName = 'someDb';

    //region Demo
    try {
        //region Step_1
        const createDatabaseOperation = new CreateDatabaseOperation({ databaseName });
        //endregion

        //region Step_2
        await store.maintenance.server.send(createDatabaseOperation);
        //endregion
    } catch (err) {
        if (err.name === 'ConcurrencyException') {
            // Database already exists
        }
    }
    //endregion
}

module.exports = { run };
