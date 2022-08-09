const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');

async function run ({ term1, term2, term3 }) {
    const session = documentStore.openSession();

    const employeesWithMatchingTerms = await session.query(Employee)
        .search('Notes', term1 + ' ' + term2, 'AND')
        .search('Notes', term3, 'OR')
        .all();

    return employeesWithMatchingTerms;
}

module.exports = { run };
