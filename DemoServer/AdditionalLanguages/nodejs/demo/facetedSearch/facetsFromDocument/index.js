const {
    AbstractCsharpIndexCreationTask,
    Facet,
    RangeFacet,
    FacetSetup
} = require('ravendb');
const { documentStore } = require('../../common/docStoreHolder');
const { Product } = require('../../common/models');

class Products_ByCategoryAndPrice extends AbstractCsharpIndexCreationTask {
    constructor () {
        super();

        this.map = 'docs.Products.Select(product => new {' +
            '    CategoryName = (this.LoadDocument(product.Category, "Categories")).Name,' +
            '    PricePerUnit = product.PricePerUnit' +
            '})';
    }
}

async function run ({ range1, range2, range3 }) {
    range1 = range1 || 25;
    range2 = range2 || 50;
    range3 = range3 || 100;

    const facetsResults = [];

    const session = documentStore.openSession();

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

    await session.store(facetSetup);
    await session.saveChanges();

    const queryResults = await session.query(Product, Products_ByCategoryAndPrice)
        .aggregateUsing('myFacetSetupDocumentID')
        .execute();

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
