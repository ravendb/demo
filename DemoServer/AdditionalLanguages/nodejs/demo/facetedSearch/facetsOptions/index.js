const {
    AbstractCsharpIndexCreationTask,
    Facet,
    FacetOptions
} = require('ravendb');
const { documentStore } = require('../../common/docStoreHolder');
const { Product } = require('../../common/models');

class Products_ByCategoryAndSupplier extends AbstractCsharpIndexCreationTask {
    constructor () {
        super();

        this.map = 'docs.Products.Select(product => new {' +
            '    CategoryName = (this.LoadDocument(product.Category, "Categories")).Name,' +
            '    Supplier = product.Supplier' +
            '})';
    }
}

async function run ({ start, pageSize, includeRemainingTerms }) {
    start = start != null ? start : 3;
    pageSize = pageSize != null ? pageSize : 2;

    const facet1 = new Facet();
    facet1.fieldName = 'CategoryName';

    const facet1Options = new FacetOptions();
    facet1Options.start = start;
    facet1Options.pageSize = pageSize;
    facet1Options.includeRemainingTerms = includeRemainingTerms;
    facet1Options.termSortMode = 'CountDesc';

    facet1.options = facet1Options;

    const facet2 = new Facet();
    facet2.fieldName = 'Supplier';

    const facet2Options = new FacetOptions();
    facet2Options.start = start;
    facet2Options.pageSize = pageSize;
    facet2Options.includeRemainingTerms = includeRemainingTerms;
    facet2Options.termSortMode = 'ValueAsc';
    facet2.options = facet2Options;

    const facets = [facet1, facet2];

    const session = documentStore.openSession();

    const queryResults = await session.query(Product, Products_ByCategoryAndSupplier)
        .aggregateBy(...facets)
        .execute();

    return queryResults;
}

module.exports = { run, Products_ByCategoryAndSupplier };
