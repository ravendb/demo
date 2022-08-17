const assert = require('assert');
const {
    documentStore,
    mediaStore
} = require('../demo/common/docStoreHolder');

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

        assert.equal(result.length, 2);
    });

    it('query search operators', async function () {
        const result = await require('../demo/textSearch/fTSQuerySearchOperators').run({
            term1: 'Spanish',
            term2: 'Portuguese',
            term3: 'Manager'
        });

        assert.equal(result.length, 3);
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

    it('highlight query results basics', async function () {
        const employees = await require('../demo/textSearch/highlightQueryResultsBasics').run({
            fragmentLength: 50,
            fragmentCount: 2
        });

        assert.ok(employees);
    });

    it('highlight query results customized', async function () {
        const { run, EmployeesDetails } = require('../demo/textSearch/highlightQueryResultsCustomized');

        await new EmployeesDetails().execute(documentStore);

        const results = await run({});

        assert.equal(results.length, 5);
    });

    it('highlight query results map reduce', async function () {
        const { run, ArtistsAllSongs } = require('../demo/textSearch/highlightQueryResultsMapReduce');

        await new ArtistsAllSongs().execute(mediaStore);

        const results = await run({});
        assert.equal(results.length, 13);
    });
});
