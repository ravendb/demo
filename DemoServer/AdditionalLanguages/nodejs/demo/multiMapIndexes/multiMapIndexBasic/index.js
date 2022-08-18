const { documentStore } = require('../../common/docStoreHolder');
//region Usings
const { AbstractJavaScriptMultiMapIndexCreationTask } = require('ravendb');
//endregion

//region Demo
//region Step_1
class CompaniesAndSuppliers_ByName extends AbstractJavaScriptMultiMapIndexCreationTask {
//endregion
    constructor () {
        super();

        //region Step_2
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
        //endregion
    }
}

//region Step_3
class IndexEntry {
    constructor () {
        this.name = null;
    }
}
//endregion
//endregion

async function run ({ namePrefix }) {
    namePrefix = namePrefix || 'A';

    //region Demo
    const session = documentStore.openSession();

    //region Step_4
    const companiesAndSuppliersNames = await session.query(IndexEntry, CompaniesAndSuppliers_ByName)
        .whereStartsWith('name', namePrefix)
        .all();
    //endregion
    //endregion

    return companiesAndSuppliersNames;
}

module.exports = { run, CompaniesAndSuppliers_ByName };
