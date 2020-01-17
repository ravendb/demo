const assert = require('assert');

describe('Static indexes', function () {
    it('map reduce index', async function () {
        const result = await require('../demo/staticIndexes/mapReduceIndex').run({
            country: 'USA'
        });

        assert.strictEqual(result, 5);
    });
});
