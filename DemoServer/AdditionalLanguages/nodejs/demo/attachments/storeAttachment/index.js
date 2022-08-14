const { documentStore } = require('../../common/docStoreHolder');
//region Usings
const fs = require('fs');
//endregion

const DEFAULT_DOCUMENT_ID = 'companies/2-A';

async function run ({ documentId, attachmentName, contentType, attachment }) {
    documentId = documentId || DEFAULT_DOCUMENT_ID;

    //region Demo
    const session = documentStore.openSession();

    //region Step_1
    const stream = fs.createReadStream(attachment);
    //endregion

    //region Step_2
    session.advanced.attachments.store(documentId, attachmentName, stream, contentType);
    //endregion

    //region Step_3
    await session.saveChanges();
    //endregion
    //endregion
}

module.exports = { run };
