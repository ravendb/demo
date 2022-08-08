const { documentStore } = require('../demo/common/docStoreHolder');

describe('Attachments', function () {
    it('index attachment details', async () => {
        const { run, Employees_ByAttachmentDetails } = require('../demo/attachments/indexAttachmentDetails');

        await new Employees_ByAttachmentDetails().execute(documentStore);

        await run({});
    });

    it('load attachment', async () => {
        const { run } = require('../demo/attachments/loadAttachment');

        await run({ attachmentName: 'image.' });
    });
});
