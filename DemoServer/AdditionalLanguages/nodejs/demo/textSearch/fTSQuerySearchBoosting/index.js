const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');

async function run ({ boost1, boost2, boost3 }) {
    const session = documentStore.openSession();

    const employeesWithMatchingTerms = await session.query(Employee)
        .search('Notes', 'ph.d.').boost(boost1)
        .search('Notes', 'university').boost(boost2)
        .search('Notes', 'college').boost(boost3)
        .all();

    return employeesWithMatchingTerms;
}

module.exports = { run };
