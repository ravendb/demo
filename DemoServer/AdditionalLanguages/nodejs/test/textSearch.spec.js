const assert = require('assert');

describe('text search', function () {
    it('single field', async function () {
        const result = await require('../demo/textSearch/fTSWithStaticIndexSingleField').run({
            searchTerm: 'fruit'
        });
        assert.ok(result[0]);
        assert.strictEqual(result.length, 1);
    });
    it('multiple fields', async function () {
        const result = await require('../demo/textSearch/fTSWithStaticIndexMultipleFields').run({
            searchTerm: 'box'
        });
        assert.ok(result && result.length);
    });
});
