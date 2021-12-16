//region Usings
//endregion
const { documentStore } = require('../../common/docStoreHolder');

async function run ({ companyName }) {
    //region Demo
    const session = documentStore.openSession();
    //region Step_1
    const company = await session.load('companies/5-A');
    //endregion

    //region Step_2
    company.name = companyName;
    //endregion

    //region Step_3
    await session.saveChanges();
    //endregion
    //endregion
}

module.exports = { run };
