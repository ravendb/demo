const { documentStore } = require('../../common/docStoreHolder');
//region Usings
const { AbstractJavaScriptMultiMapIndexCreationTask } = require('ravendb');
//endregion

//region Demo
//region Step_1
class Contacts_ByNameAndTitle extends AbstractJavaScriptMultiMapIndexCreationTask {
//endregion
    constructor () {
        super();

        const { getMetadata } = this.mapUtils();

        //region Step_2
        this.map('Employees', employee => {
            return {
                contactName: employee.FirstName + ' ' + employee.LastName,
                contactTitle: employee.Title,
                collection: getMetadata(employee)['@collection']
            };
        });

        this.map('Companies', company => {
            return {
                contactName: company.Contact.Name,
                contactTitle: company.Contact.Title,
                collection: getMetadata(company)['@collection']
            };
        });

        this.map('Suppliers', supplier => {
            return {
                contactName: supplier.Contact.Name,
                contactTitle: supplier.Contact.Title,
                collection: getMetadata(supplier)['@collection']
            };
        });
        //endregion

        //region Step_3
        this.store('contactName', 'Yes');
        this.store('contactTitle', 'Yes');
        this.store('collection', 'Yes');
        //endregion
    }
}

//region Step_4
class IndexEntry {
    constructor () {
        this.contactName = null;
        this.contactTitle = null;
        this.collection = null;
    }
}
//endregion

//region Step_5
class ProjectedEntry extends IndexEntry {
    constructor () {
        super();
        this.phone = null;
    }
}
//endregion
//endregion

async function run ({ namePrefix, titlePrefix }) {
    namePrefix = namePrefix || 'Michael';
    titlePrefix = titlePrefix || 'Sales';

    //region Demo
    const session = documentStore.openSession();
    //region Step_6
    const contacts = await session.query(IndexEntry, Contacts_ByNameAndTitle)
        .whereStartsWith('contactName', namePrefix)
        .whereStartsWith('contactTitle', titlePrefix)
        .selectFields(['contactName', 'contactTitle', 'collection', 'phone'], ProjectedEntry)
        .all();
    //endregion
    //endregion

    return contacts;
}

module.exports = { run, Contacts_ByNameAndTitle };
