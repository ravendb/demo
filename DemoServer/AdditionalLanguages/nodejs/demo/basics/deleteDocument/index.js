//region Usings
//endregion
const { documentStore } = require('../common/docStoreHolder');

async function run ({ documentID }) {
    //region Demo
    const session = documentStore.openSession();
    //region Step_1
    session.delete(documentID);
    //endregion

    //region Step_2
    await session.saveChanges();
    //endregion
    //endregion
}

module.export = { run };
