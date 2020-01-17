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
    //endregion
    //endregion

    //region Demo
    const newContact = new Contact();
    newContact.name = contactName;
    newContact.title = contactTitle;

    //region Step_2
    newCompany.contact = newContact;
    //endregion
    //endregion

    //region Demo
    const session = store.openSession();
    //region Step_3
    await session.store(newCompany);
    //endregion

    //region Step_4
    const theNewDocumentId = newCompany.id;
    //endregion

    //region Step_5
    await session.saveChanges();
    //endregion
    //endregion
}

module.exports = { run };
