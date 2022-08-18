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
                CategoryName: load(product.Category, "Categories").Name,
                PricePerUnit: product.PricePerUnit,
                UnitsInStock: product.UnitsInStock
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
    facet.fieldName = 'CategoryName';

    const pricePerUnitAggregationField = new FacetAggregationField();
    pricePerUnitAggregationField.name = 'PricePerUnit';

    const unitsInStockAggregationField = new FacetAggregationField();
    unitsInStockAggregationField.name = 'UnitsInStock';

    facet.aggregations.set('Average', [pricePerUnitAggregationField]);
    facet.aggregations.set('Sum', [unitsInStockAggregationField]);
    facet.aggregations.set('Max', [pricePerUnitAggregationField]);
    const fields = [pricePerUnitAggregationField, unitsInStockAggregationField];
    facet.aggregations.set('Min', fields);
    //endregion

    //region Step_3
    const rangeFacet = new RangeFacet();
    rangeFacet.ranges = [
        'PricePerUnit < ' + range1,
        'PricePerUnit >= ' + range1 + ' and PricePerUnit < ' + range2,
        'PricePerUnit >= ' + range2 + ' and PricePerUnit < ' + range3,
        'PricePerUnit >= ' + range3
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
