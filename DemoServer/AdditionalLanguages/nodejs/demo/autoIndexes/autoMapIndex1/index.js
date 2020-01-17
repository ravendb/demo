const { store } = require('../../common/docStoreHolder');

async function run ({ firstName }) {
    //region Demo
    const session = store.openSession();
    //region Step_1
    const findEmployeeQuery = session.query({ collection: 'employees' })
        .whereEquals('FirstName', firstName);
    //endregion

    //region Step_2
    const employeeResult = await findEmployeeQuery.firstOrNull();
    //endregion
    //endregion
}

module.exports = { run };
