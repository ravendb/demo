const { store } = require('../../common/docStoreHolder');

async function run ({ country }) {
    //region Demo
    const session = store.openSession();
    //region Step_1
    const findEmployeeQuery = session.query({ collection: 'employees' })
        .whereEquals('Address.Country', country)
        .whereStartsWith('Title', 'Sales');
    //endregion

    //region Step_2
    const employeeResult = await findEmployeeQuery.firstOrNull();
    //endregion
    //endregion
}

module.exports = { run };
