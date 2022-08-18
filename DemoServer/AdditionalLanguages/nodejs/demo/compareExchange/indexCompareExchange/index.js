//region Usings
const { AbstractCsharpIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');
const { Product } = require('../../common/models');

//region Demo
//region Step_1
class Products_ByUnitsInStock extends AbstractCsharpIndexCreationTask {
//endregion
    constructor () {
        super();

        //region Step_2
        this.map = 'docs.Products.Select(product => new {' +
            '    UnitsInStock = this.LoadCompareExchangeValue(Id(product))' +
            '})';
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
        .whereGreaterThan('UnitsInStock', minValue)
        .all();
    //endregion
    //endregion
}

module.exports = { run, Products_ByUnitsInStock };
