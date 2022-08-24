//region Usings
const {
    Facet,
    RangeFacet,
    FacetSetup,
    AbstractJavaScriptIndexCreationTask
} = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');
const { Product } = require('../../common/models');

//region Demo
//region Step_1
class Products_ByCategoryAndPrice extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        const { load } = this.mapUtils();

        this.map("Products", product => {
            return {
                category: load(product.Category, "Categories").Name,
                pricePerUnit: product.PricePerUnit
            }
        });
    }
}
//endregion
//endregion

async function run ({ range1, range2, range3 }) {
    range1 = range1 || 25;
    range2 = range2 || 50;
    range3 = range3 || 100;

    //region Demo
    const facetsResults = [];

    const session = documentStore.openSession();

    //region Step_2
    const facetSetup = new FacetSetup();
    facetSetup.id = 'myFacetSetupDocumentID';

    const facet = new Facet();
    facet.fieldName = 'category';

    facetSetup.facets = [facet];

    const rangeFacet = new RangeFacet();
    rangeFacet.ranges = [
        'pricePerUnit < ' + range1,
        'pricePerUnit >= ' + range1 + ' and pricePerUnit < ' + range2,
        'pricePerUnit >= ' + range2 + ' and pricePerUnit < ' + range3,
        'pricePerUnit >= ' + range3
    ];

    facetSetup.rangeFacets = [rangeFacet];
    //endregion

    //region Step_3
    await session.store(facetSetup);
    await session.saveChanges();
    //endregion

    //region Step_4
    const queryResults = await session.query(Product, Products_ByCategoryAndPrice)
        .aggregateUsing('myFacetSetupDocumentID')
        .execute();
    //endregion
    //endregion

    Object.values(queryResults).forEach(result => {
        const facetName = result.name;

        result.values.forEach(item => {
            facetsResults.push({
                facetName: facetName, // i.e. pricePerUnit
                facetRange: item.range, // i.e. pricePerUnit < 50
                facetCount: item.count
            });
        });
    });

    return facetsResults;
}

module.exports = { run, Products_ByCategoryAndPrice };
