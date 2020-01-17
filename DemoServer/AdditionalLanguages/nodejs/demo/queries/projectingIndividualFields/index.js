//region Usings
const { QueryData } = require('ravendb');
//endregion

const { store } = require('../../common/docStoreHolder');

async function run () {
    //region Demo
    const session = store.openSession();
    //region Step_1
    const projectedQuery = session.query({ collection: 'companies' })
    //endregion
    //region Step_2
        .selectFields(new QueryData(
            ['Name', 'Address.City', 'Address.Country'],
            ['companyName', 'city', 'country']));
    //endregion

    //region Step_3
    const projectedResults = await projectedQuery.all();
    // [
    //     {
    //       companyName: 'Alfreds Futterkiste',
    //       city: 'Berlin',
    //       country: 'Germany',
    //       id: 'companies/1-A'
    //     },
    //     {
    //       companyName: 'Ana Trujillo Emparedados y helados',
    //       city: 'México D.F.',
    //       country: 'Mexico',
    //       id: 'companies/2-A'
    //     },
    //     ...
    // [
    //endregion
    //endregion
    return projectedResults;
}

module.exports = { run };
