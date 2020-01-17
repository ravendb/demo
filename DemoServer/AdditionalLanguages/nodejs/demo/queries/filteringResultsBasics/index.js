const { store } = require('../../common/docStoreHolder');

async function run () {
    //region Demo
    const session = store.openSession();
    //region Step_1
    const filteredQuery = session.query({ collection: 'employees' })
    //endregion
        //region Step_2
        .whereEquals('FirstName', 'Anne');
        //endregion

    //region Step_3
    const filteredEmployees = await filteredQuery.all();
    //endregion
    //endregion
}

module.exports = { run };
