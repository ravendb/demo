//region Usings
const {
    AbstractCsharpIndexCreationTask,
    Facet,
    RangeFacet,
    FacetSetup
} = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');
const { Product } = require('../../common/models');

//region Demo
//region Step_1
class Products_ByCategoryAndPrice extends AbstractCsharpIndexCreationTask {
    constructor () {
        super();

        this.map = 'docs.Products.Select(product => new {' +
            '    CategoryName = (this.LoadDocument(product.Category, "Categories")).Name,' +
            '    PricePerUnit = product.PricePerUnit' +
            '})';
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
    facet.fieldName = 'Category';

    facetSetup.facets = [facet];

    const rangeFacet = new RangeFacet();
    rangeFacet.ranges = [
        'PricePerUnit < ' + range1,
        'PricePerUnit >= ' + range1 + ' and PricePerUnit < ' + range2,
        'PricePerUnit >= ' + range2 + ' and PricePerUnit < ' + range3,
        'PricePerUnit >= ' + range3
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
                facetName: facetName, // i.e. PricePerUnit
                facetRange: item.range, // i.e. PricePerUnit < 50
                facetCount: item.count
            });
        });
    });

    return facetsResults;
}

module.exports = { run, Products_ByCategoryAndPrice };
