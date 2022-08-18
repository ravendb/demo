const { documentStore } = require('../../common/docStoreHolder');

const DEFAULT_DOCUMENT_ID = 'categories/1-A';

async function run ({ documentId, attachmentName }) {
    documentId = documentId || DEFAULT_DOCUMENT_ID;

    //region Demo
    const session = documentStore.openSession();

    //region Step_1
    const attachmentExists = await session.advanced.attachments.exists(documentId, attachmentName);
    //endregion

    if (attachmentExists) {
        //region Step_2
        const attachmentResult = await session.advanced.attachments.get(documentId, attachmentName);
        //endregion

        //region Step_3
        const contentType = attachmentResult.details.contentType;
        const hash = attachmentResult.details.hash;
        const size = attachmentResult.details.size;
        //endregion

        //region Step_4
        const data = attachmentResult.data;
        //endregion
    }
    //endregion
}

module.exports = { run };
