//region Usings
const {    
    ConfigureRevisionsOperation,
    RevisionsConfiguration,
    RevisionsCollectionConfiguration
} = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');

async function run ({ collection1, collection2 }) {
    //region Demo
    //region Step_1
    const myRevisionsConfiguration = new RevisionsConfiguration();
    //endregion

    //region Step_2
    const defaultConfiguration = new RevisionsCollectionConfiguration();
    defaultConfiguration.disabled = false;
    defaultConfiguration.purgeOnDelete = false;
    defaultConfiguration.minimumRevisionsToKeep = 5;
    defaultConfiguration.minimumRevisionAgeToKeep = '14.00:00:00';

    myRevisionsConfiguration.defaultConfig = defaultConfiguration;
    //endregion

    //region Step_3
    const collection1Configuration = new RevisionsCollectionConfiguration();
    collection1Configuration.disabled = true;

    const collection2Configuration = new RevisionsCollectionConfiguration();
    collection2Configuration.purgeOnDelete = true;

    myRevisionsConfiguration.collections = new Map([
        [collection1, collection1Configuration],
        [collection2, collection2Configuration]
    ]);
    //endregion

    //region Step_4
    const revisionsConfigurationOperation = new ConfigureRevisionsOperation(myRevisionsConfiguration);
    await documentStore.maintenance.send(revisionsConfigurationOperation);
    //endregion
    //endregion
}

module.exports = { run };
