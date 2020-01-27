//region Usings
const { QueryData } = require('ravendb');
//endregion

const { documentStore } = require('../../common/docStoreHolder');

async function run () {
    //region Demo
    const session = documentStore.openSession();
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
    //endregion
    //endregion
    return projectedResults;
}

module.exports = { run };
