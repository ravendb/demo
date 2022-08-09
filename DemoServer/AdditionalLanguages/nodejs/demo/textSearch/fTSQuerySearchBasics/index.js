const { mediaStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');

async function run ({ term1, term2 }) {
    const session = mediaStore.openSession();

    const employeesWithMatchingTerms = await session.query(Employee)
        .search('Notes', term1 + ' ' + term2)
        .all();

    return employeesWithMatchingTerms;
}

module.exports = { run };
