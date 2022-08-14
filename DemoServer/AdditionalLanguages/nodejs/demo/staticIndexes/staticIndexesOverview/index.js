//region Usings
const { AbstractCsharpIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');

//region Demo
//region Step_1
class Employees_ByLastName extends AbstractCsharpIndexCreationTask {
//endregion

    //region Step_2
    constructor () {
        super();
        // Define:
        //    Map(s) functions
        //    Reduce function
        //    Additional indexing options per field
    }
    //endregion
}
//endregion

async function run () {
    //region Demo
    //region Step_3
    await new Employees_ByLastName().execute(documentStore);
    //endregion

    const session = documentStore.openSession();
    //region Step_4
    const queryOnIndex = session.query({ indexName: 'Employees/ByLastName' })
        .whereEquals('LastName', 'SomeName');

    const queryResults = await queryOnIndex.all();
    //endregion
    //endregion
}

module.exports = { run };
