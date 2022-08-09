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

    it('query search basics', async function () {
        const result = await require('../demo/textSearch/fTSQuerySearchBasics').run({
            term1: 'Washington',
            term2: 'Colorado'
        });

        assert.ok(result);
    });

    it('query search operators', async function () {
        const result = await require('../demo/textSearch/fTSQuerySearchOperators').run({
            term1: 'Spanish',
            term2: 'Portuguese',
            term3: 'Manager'
        });

        assert.ok(result);
    });

    it('query search wildcards', async function () {
        const result = await require('../demo/textSearch/fTSQuerySearchWildcards').run({
            start: 'ma',
            end: 'lin',
            middle: 'oliv',
            numberOrResults: 10
        });

        assert.ok(result);
    });
});
