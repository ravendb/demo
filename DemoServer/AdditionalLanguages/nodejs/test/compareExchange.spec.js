
describe('Compare Exchange', function () {
    it('create compare exchange', async () => {
        const { run } = require('../demo/compareExchange/createCompareExchange');

        await run({});
    });

    it('index compare exchange', async () => {
        const { run } = require('../demo/compareExchange/indexCompareExchange');

        await run({});
    });
});
