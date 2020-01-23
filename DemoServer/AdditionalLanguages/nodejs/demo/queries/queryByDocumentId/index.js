const { documentStore } = require('../../common/docStoreHolder');

async function run ({ employeeDocumentId }) {
    //region Demo
    const session = documentStore.openSession();
    //region Step_1
    const queryByDocumentId = session.query({ collection: 'employees' })
    //endregion
    //region Step_2
        .whereEquals('id', employeeDocumentId);
    //endregion

    //region Step_3
    const employee = await queryByDocumentId.firstOrNull();
    //endregion
    //endregion

    return employee;
}

module.exports = { run };
