/* eslint-disable no-unused-vars */
//region Usings
//endregion
const { store } = require('../../common/docStoreHolder');
const { Company, Contact } = require('../../common/models');

async function run ({ companyName, companyPhone, contactName, contactTitle }) {
    //region Demo
    //region Step_1
    const newCompany = new Company();
    newCompany.name = companyName;
    newCompany.phone = companyPhone;

    const newContact = new Contact();
    newContact.name = contactName;
    newContact.title = contactTitle;

    newCompany.contact = newContact;
    //endregion

    const session = store.openSession();
    //region Step_2
    await session.store(newCompany);
    //endregion

    //region Step_3
    const theNewDocumentId = newCompany.id;
    //endregion

    //region Step_4
    await session.saveChanges();
    //endregion
    //endregion
}

module.exports = { run };
