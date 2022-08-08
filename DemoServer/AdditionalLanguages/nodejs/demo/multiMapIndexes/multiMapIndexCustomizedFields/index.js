const { documentStore } = require('../../common/docStoreHolder');
const { AbstractJavaScriptMultiMapIndexCreationTask } = require('ravendb');

class Contacts_ByNameAndTitle extends AbstractJavaScriptMultiMapIndexCreationTask {
    constructor () {
        super();

        const { getMetadata } = this.mapUtils();

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

        this.store('contactName', 'Yes');
        this.store('contactTitle', 'Yes');
        this.store('collection', 'Yes');
    }
}

class IndexEntry {
    constructor () {
        this.contactName = null;
        this.contactTitle = null;
        this.collection = null;
    }
}

class ProjectedEntry extends IndexEntry {
    constructor () {
        super();
        this.phone = null;
    }
}

async function run ({ namePrefix, titlePrefix }) {
    namePrefix = namePrefix || 'Michael';
    titlePrefix = titlePrefix || 'Sales';

    const session = documentStore.openSession();
    const contacts = await session.query(IndexEntry, Contacts_ByNameAndTitle)
        .whereStartsWith('contactName', namePrefix)
        .whereStartsWith('contactTitle', titlePrefix)
        .selectFields(['contactName', 'contactTitle', 'collection', 'phone'], ProjectedEntry)
        .all();

    return contacts;
}

module.exports = { run, Contacts_ByNameAndTitle };
