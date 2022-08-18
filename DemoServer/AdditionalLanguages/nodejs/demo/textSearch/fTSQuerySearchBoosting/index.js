const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');

async function run ({ boost1, boost2, boost3 }) {
    //region Demo
    const session = documentStore.openSession();

    //region Step_1
    const employeesWithMatchingTerms = await session.query(Employee)
    //endregion
        //region Step_2
        .search('Notes', 'ph.d.').boost(boost1)
        .search('Notes', 'university').boost(boost2)
        .search('Notes', 'college').boost(boost3)
        //endregion
        //region Step_3
        .all();
        //endregion
    //endregion

    return employeesWithMatchingTerms;
}

module.exports = { run };
