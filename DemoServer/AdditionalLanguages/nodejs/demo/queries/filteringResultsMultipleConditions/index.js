//region Usings
//endregion
const { store } = require('../../common/docStoreHolder');

async function run ({ country }) {
    //region Demo
    const session = store.openSession();
    //region Step_1
    const filteredQuery = session.query({ collection: 'employees' })
    //endregion
    //region Step_2
        .whereIn('FirstName', ['Anne', 'John'])
        .orElse()
        .openSubclause()
        .whereEquals('Address.Country', country)
        .whereGreaterThan('Territories.Count', 2)
        .whereStartsWith('Title', 'Sales')
        .closeSubclause();
    //endregion

    //region Step_3
    const filteredEmployees = await filteredQuery.all();
    //endregion
    //endregion
}

module.exports = { run };
