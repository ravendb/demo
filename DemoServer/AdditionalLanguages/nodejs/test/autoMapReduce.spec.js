const assert = require('assert');

describe('autoIndexes', function () {
    it('auto map-reduce index', async function () {
        const result = await require('../demo/autoIndexes/autoMapReduceIndex').run();
        assert.ok(result);
        assert.ok(result[0].Country);
        assert.ok(result[0].NumberOfEmployees > 0);
    });
});
