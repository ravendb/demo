//region Usings
const { Facet, FacetOptions, AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');
const { Product } = require('../../common/models');

//region Demo
//region Step_1
class Products_ByCategoryAndSupplier extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        const { load } = this.mapUtils();

        this.map("Products", product => {
            return {
                CategoryName: load(product.Category, "Categories").Name,
                Supplier: product.Supplier
            }
        });
    }
}
//endregion
//endregion

async function run ({ start, pageSize, includeRemainingTerms }) {
    start = start != null ? start : 3;
    pageSize = pageSize != null ? pageSize : 2;

    //region Demo
    //region Step_2
    const facet1 = new Facet();
    facet1.fieldName = 'CategoryName';

    const facet1Options = new FacetOptions();

    facet1Options.start = start;
    facet1Options.pageSize = pageSize;
    facet1Options.includeRemainingTerms = includeRemainingTerms;
    facet1Options.termSortMode = 'CountDesc';

    facet1.options = facet1Options;
    //endregion

    const facet2 = new Facet();
    facet2.fieldName = 'Supplier';

    const facet2Options = new FacetOptions();

    facet2Options.start = start;
    facet2Options.pageSize = pageSize;
    facet2Options.includeRemainingTerms = includeRemainingTerms;
    facet2Options.termSortMode = 'ValueAsc';

    facet2.options = facet2Options;

    //region Step_3
    const facets = [facet1, facet2];
    //endregion

    const session = documentStore.openSession();

    //region Step_4
    const queryResults = await session.query(Product, Products_ByCategoryAndSupplier)
        .aggregateBy(...facets)
        .execute();
    //endregion
    //endregion

    return queryResults;
}

module.exports = { run, Products_ByCategoryAndSupplier };
