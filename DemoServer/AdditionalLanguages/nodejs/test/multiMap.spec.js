const { documentStore } = require('../demo/common/docStoreHolder');

describe('MultiMap', function () {
    it('Multi map basic', async () => {
        const { run, CompaniesAndSuppliers_ByName } = require('../demo/multiMapIndexes/multiMapIndexBasic');

        await new CompaniesAndSuppliers_ByName().execute(documentStore);

        await run({});
    });

    it('Customized fields', async () => {
        const { run, Contacts_ByNameAndTitle } = require('../demo/multiMapIndexes/multiMapIndexCustomizedFields');

        await new Contacts_ByNameAndTitle().execute(documentStore);

        const list = await run({});
        console.log(list);
    });

    it('Multi map reduce', async () => {
        const { run, CityCommerceDetails } = require('../demo/multiMapIndexes/multiMapReduceIndex');

        await new CityCommerceDetails().execute(documentStore);

        const results = await run({});
        console.log(results);
    });
});
