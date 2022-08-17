const { documentStore } = require('../demo/common/docStoreHolder');
const assert = require('assert');

describe('Faceted Search', function () {
    it('aggregations', async () => {
        const { run, Products_ByCategoryPriceAndUnits } = require('../demo/facetedSearch/facetsAggregations');

        await new Products_ByCategoryPriceAndUnits().execute(documentStore);

        const results = await run({});

        assert.equal(results.CategoryName.values.length, 16);
        assert.equal(results.PricePerUnit.values.length, 8);
    });

    it('basics', async () => {
        const { run, Products_ByCategoryAndPrice } = require('../demo/facetedSearch/facetsBasics');

        await new Products_ByCategoryAndPrice().execute(documentStore);

        const facetResults = await run({});
        assert.equal(facetResults.length, 12);
    });

    it('from document', async () => {
        const { run, Products_ByCategoryAndPrice } = require('../demo/facetedSearch/facetsFromDocument');

        await new Products_ByCategoryAndPrice().execute(documentStore);

        const facetResults = await run({});

        assert.equal(facetResults.length, 12); // TODO: This fails. Missing 8 Category items.
    });

    it('facet options', async () => {
        const { run, Products_ByCategoryAndSupplier } = require('../demo/facetedSearch/facetsOptions');

        await new Products_ByCategoryAndSupplier().execute(documentStore);

        const results = await run({});

        assert.equal(results.CategoryName.values.length, 2);
        assert.equal(results.Supplier.values.length, 2);
    });
});
