const { documentStore } = require('../../common/docStoreHolder');

async function run () {
    //region Demo
    const session = documentStore.openSession();
    //region Step_1
    const queryDefinition = session.query({ collection: 'employees' });
    //endregion

    //region Step_2
    // Define actions such as:

    // Filter documents by documents fields
    // Filter documents by text criteria
    // Include related documents
    // Get the query stats
    // Sort results
    // Customize the returned entity fields (Projections)
    // Control results paging
    //endregion

    //region Step_3
    const queryResults = await queryDefinition.all();
    //endregion
    //endregion
}

module.exports = { run };
