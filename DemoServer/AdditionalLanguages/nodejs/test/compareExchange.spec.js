const assert = require('assert');

describe('Compare Exchange', function () {
    it('create compare exchange', async () => {
        const { run } = require('../demo/compareExchange/createCompareExchange');

        await run({});
    });

    it('index compare exchange', async () => {
        const { run } = require('../demo/compareExchange/indexCompareExchange');

        const result = await run({
            minValue: 25
        });

        assert.equal(result.length, 7);
    });
});
