const { documentStore } = require('../../common/docStoreHolder');

async function run () {
    //region Demo
    const session = documentStore.openSession();
    //region Step_1
    const fullCollectionQuery = session.query({ collection: 'companies' });
    //endregion

    //region Step_2
    const collectionResults = await fullCollectionQuery.all();
    //endregion
    //endregion
}

module.exports = { run };
