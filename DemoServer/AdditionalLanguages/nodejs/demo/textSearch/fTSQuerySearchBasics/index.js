const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');

async function run ({ term1, term2 }) {
    //region Demo
    const session = documentStore.openSession();

    //region Step_1
    const employeesWithMatchingTerms = await session.query(Employee)
    //endregion
        //region Step_2
        .search('Notes', term1 + ' ' + term2)
        //endregion
        //region Step_3
        .all();
        //endregion
    //endregion

    return employeesWithMatchingTerms;
}

module.exports = { run };
