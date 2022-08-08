const assert = require('assert');

describe('JavaScript Map Index', function () {
    it('map', async () => {
        const { run } = require('../demo/javascriptIndexes/javascriptMapIndex');

        const results = await run({});
        assert.ok(results);
    });
});
