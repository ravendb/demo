const assert = require('assert');

describe('Queries', function () {
    it('projecting into individual fields', async function () {
        const result = await require('../demo/queries/projectingIndividualFields').run();
        assert.ok(result);
        assert.ok(result[0].city);
        assert.ok(result[0].companyName);
        assert.ok(result[0].country);
    });

    it('query by doc id', async function () {
        const result = await require('../demo/queries/queryByDocumentId').run({
            employeeDocumentId: 'employees/1-A'
        });
        assert.ok(result);
    });
});
