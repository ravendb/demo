const {
    AbstractCsharpIndexCreationTask
} = require('ravendb');
const { documentStore } = require('../../common/docStoreHolder');
const { Product } = require('../../common/models');

class Products_ByUnitsInStock extends AbstractCsharpIndexCreationTask {
    constructor () {
        super();

        this.map = 'docs.Products.Select(product => new {\n' +
            '    UnitsInStock = this.LoadCompareExchangeValue(Id(product))\n' +
            '})';
    }
}

async function run ({ minValue }) {
    await new Products_ByUnitsInStock().execute(documentStore);

    const session = documentStore.openSession();

    return session.query(Product, Products_ByUnitsInStock)
        .whereGreaterThan('UnitsInStock', minValue)
        .all();
}

module.exports = { run, Products_ByUnitsInStock };
