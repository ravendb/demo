//region Usings
const {
    Facet,
    FacetAggregationField,
    RangeFacet,
    AbstractJavaScriptIndexCreationTask
} = require('ravendb');
//endregion
const { documentStore } = require('../../common/docStoreHolder');
const { Product } = require('../../common/models');

//region Demo
//region Step_1
class Products_ByCategoryPriceAndUnits extends AbstractJavaScriptIndexCreationTask {
    constructor () {
        super();

        const { load } = this.mapUtils();

        this.map("Products", product => {
            return {
                categoryName: load(product.Category, "Categories").Name,
                pricePerUnit: product.PricePerUnit,
                unitsInStock: product.UnitsInStock
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
    //region Step_2
    const facet = new Facet();
    facet.fieldName = 'categoryName';

    const pricePerUnitAggregationField = new FacetAggregationField();
    pricePerUnitAggregationField.name = 'pricePerUnit';

    const unitsInStockAggregationField = new FacetAggregationField();
    unitsInStockAggregationField.name = 'unitsInStock';

    facet.aggregations.set('Average', [pricePerUnitAggregationField]);
    facet.aggregations.set('Sum', [unitsInStockAggregationField]);
    facet.aggregations.set('Max', [pricePerUnitAggregationField]);
    const fields = [pricePerUnitAggregationField, unitsInStockAggregationField];
    facet.aggregations.set('Min', fields);
    //endregion

    //region Step_3
    const rangeFacet = new RangeFacet();
    rangeFacet.ranges = [
        'pricePerUnit < ' + range1,
        'pricePerUnit >= ' + range1 + ' and pricePerUnit < ' + range2,
        'pricePerUnit >= ' + range2 + ' and pricePerUnit < ' + range3,
        'pricePerUnit >= ' + range3
    ];

    rangeFacet.aggregations.set('Average', [pricePerUnitAggregationField]);
    rangeFacet.aggregations.set('Sum', [unitsInStockAggregationField]);
    rangeFacet.aggregations.set('Max', [pricePerUnitAggregationField]);
    rangeFacet.aggregations.set('Min', fields);
    //endregion

    //region Step_4
    const facets = [facet, rangeFacet];
    //endregion

    const session = documentStore.openSession();

    //region Step_5
    const queryResults = await session.query(Product, Products_ByCategoryPriceAndUnits)
        .aggregateBy(...facets)
        .execute();
    //endregion
    //endregion

    return queryResults;
}

module.exports = { run, Products_ByCategoryPriceAndUnits };
