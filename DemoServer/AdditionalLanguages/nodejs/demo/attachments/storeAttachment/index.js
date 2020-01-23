const { documentStore } = require('../../common/docStoreHolder');

//region Usings
//endregion

const DEFAULT_DOCUMENT_ID = 'companies/2-A';

async function run ({ documentId, attachmentName, contentType, attachment }) {
    documentId = documentId || DEFAULT_DOCUMENT_ID;

    //region Demo
    const session = documentStore.openSession();
    //region Step_1
    // attachment can be a Buffer or Node.js Readable
    //endregion

    //region Step_2
    session.advanced.attachments.store(documentId, attachmentName, attachment, contentType);
    //endregion

    //region Step_3
    await session.saveChanges();
    //endregion
}
//endregion

module.exports = { run };
