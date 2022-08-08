const assert = require('assert');
const { documentStore } = require('../demo/common/docStoreHolder');

describe('Static indexes', function () {
    it('map reduce index', async function () {
        const result = await require('../demo/staticIndexes/mapReduceIndex').run({
            country: 'USA'
        });

        assert.strictEqual(result, 5);
    });

    it('additional sources', async function () {
        const { run, Products_ByPrice } = require('../demo/staticIndexes/additionalSourcesIndex');

        await new Products_ByPrice().execute(documentStore);

        const results = await run({});
        assert.equal(results.length, 3);
    });

    it('fanout index', async function () {
        const { run, Orders_ByProductDetails } = require('../demo/staticIndexes/fanoutIndex');

        await new Orders_ByProductDetails().execute(documentStore);

        const orders = await run({});

        assert.equal(orders.length, 6);
    });

    it('project index results', async function () {
        const year = 1993;

        const { run } = require('../demo/staticIndexes/projectIndexResults');

        const result = await run({ year });

        assert.equal(result.length, 9);
    });

    it('store fields in index', async function () {
        const { run } = require('../demo/staticIndexes/storeFieldInIndex');

        const companyId = 'companies/1-A';

        const orders = await run({ companyId });

        assert.ok(orders);
    });
});
