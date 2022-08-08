const { documentStore } = require('../../common/docStoreHolder');
const { AbstractJavaScriptMultiMapIndexCreationTask } = require('ravendb');

class CompaniesAndSuppliers_ByName extends AbstractJavaScriptMultiMapIndexCreationTask {
    constructor () {
        super();

        this.map('Companies', company => {
            return {
                name: company.Name
            };
        });

        this.map('Suppliers', supplier => {
            return {
                name: supplier.Name
            };
        });
    }
}

class IndexEntry {
    constructor () {
        this.name = null;
    }
}

async function run ({ namePrefix }) {
    namePrefix = namePrefix || 'A';

    const session = documentStore.openSession();
    const companiesAndSuppliersNames = await session.query(IndexEntry, CompaniesAndSuppliers_ByName)
        .whereStartsWith('name', namePrefix)
        .all();

    return companiesAndSuppliersNames;
}

module.exports = { run, CompaniesAndSuppliers_ByName };
