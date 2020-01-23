//region Usings
//endregion
const { documentStore } = require('../common/docStoreHolder');

async function run ({ documentId }) {
    //region Demo
    const session = documentStore.openSession();
    //region Step_1
    session.delete(documentId);
    //endregion

    //region Step_2
    await session.saveChanges();
    //endregion
    //endregion
}

module.export = { run };
