const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');

async function run ({ term1, term2, term3 }) {

    //region Demo
    const session = documentStore.openSession();

    //region Step_1
    const employeesWithMatchingTerms = await session.query(Employee)
    //endregion
        //region Step_2
        .search('Notes', term1 + ' ' + term2, 'AND')
        //endregion
        //region Step_3
        .search('Notes', term3, 'OR')
        //endregion
        //region Step_4
        .all();
        //endregion
    //endregion

    return employeesWithMatchingTerms;
}

module.exports = { run };
