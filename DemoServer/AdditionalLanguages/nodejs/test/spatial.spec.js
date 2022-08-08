
const assert = require('assert');
const { documentStore } = require('../demo/common/docStoreHolder');

describe('Spatial', function () {
    it('Spatial Index', async function () {
        const {
            run,
            Companies_ByLocation
        } = require('../demo/spatial/spatialIndex');

        await new Companies_ByLocation().execute(documentStore);

        const companies = await run();

        assert.equal(companies.length, 4);
    });

    it('Spatial Query', async function () {
        const {
            run
        } = require('../demo/spatial/spatialQuery');

        const results = await run({ radius: 2 });

        assert.equal(results.length, 2);
    });
});
