//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');
const { Product } = require('../../common/models');

//region Demo
//region Step_1
class Products_ByUnitsInStock extends AbstractJavaScriptIndexCreationTask {
//endregion
    constructor () {
        super();

        const { cmpxchg, id } = this.mapUtils();

        //region Step_2
        this.map("Products", product => {
            return {
                unitsInStock: cmpxchg(id(product))
            }
        })
        //endregion
    }
}
//endregion

async function run ({ minValue }) {
    await new Products_ByUnitsInStock().execute(documentStore);

    //region Demo
    const session = documentStore.openSession();

    //region Step_3
    return session.query(Product, Products_ByUnitsInStock)
        .whereGreaterThan('unitsInStock', minValue)
        .all();
    //endregion
    //endregion
}

module.exports = { run, Products_ByUnitsInStock };
