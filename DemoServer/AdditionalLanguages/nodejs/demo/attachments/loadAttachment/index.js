const { documentStore } = require('../../common/docStoreHolder');

const DEFAULT_DOCUMENT_ID = 'categories/1-A';

async function run ({ documentId, attachmentName }) {
    documentId = documentId || DEFAULT_DOCUMENT_ID;

    const session = documentStore.openSession();

    const attachmentExists = await session.advanced.attachments.exists(documentId, attachmentName);

    if (attachmentExists) {
        const attachmentResult = await session.advanced.attachments.get(documentId, attachmentName);
        const contentType = attachmentResult.details.contentType;
        const hash = attachmentResult.details.hash;
        const size = attachmentResult.details.size;

        const data = attachmentResult.data;
    }
}

module.exports = { run };

