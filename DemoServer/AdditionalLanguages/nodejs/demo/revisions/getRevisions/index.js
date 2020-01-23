const {
    ConfigureRevisionsOperation,
    RevisionsConfiguration,
    RevisionsCollectionConfiguration
} = require('ravendb');
const { documentStore } = require('../../common/docStoreHolder');

async function run () {
    //region Demo
    //region Step_1
    const myRevisionsConfiguration = new RevisionsConfiguration();

    const defaultConfiguration = new RevisionsCollectionConfiguration();
    defaultConfiguration.disabled = false;
    myRevisionsConfiguration.defaultConfig = defaultConfiguration;

    const revisionsConfigurationOperation = new ConfigureRevisionsOperation(myRevisionsConfiguration);
    await documentStore.maintenance.send(revisionsConfigurationOperation);
    //endregion

    const session = documentStore.openSession();
    //region Step_2
    const company = await session.load('companies/7-A');

    company.name = 'Name 1';
    session.countersFor('companies/7-A').increment('MyCounter', 100);
    await session.saveChanges();

    company.name = 'Name 2';
    company.phone = '052-1234-567';
    await session.saveChanges();
    //endregion

    //region Step_3
    const revisions = await session
        .advanced
        .revisions
        .getFor('companies/7-A', 0, 25);
    //endregion
    //endregion
}
