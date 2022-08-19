const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');

//region Usings
const { AbstractJavaScriptIndexCreationTask } = require('ravendb');
//endregion

const DEFAULT_ATTACHMENT_CONTENT_TYPE = 'image/jpeg';
const DEFAULT_ATTACHMENT_MIN_SIZE = 18000;

//region Demo
//region Step_1
class Employees_ByAttachmentDetails extends AbstractJavaScriptIndexCreationTask {
//endregion
    constructor () {
        super();

        const { attachmentsFor } = this.mapUtils();

        //region Step_2
        this.map("Employees", employee => {
            const attachments = attachmentsFor(employee);
            //endregion
            //region Step_3
            return {
                attachmentNames: attachments.map(x => x.Name),
                attachmentContentTypes: attachments.map(x => x.ContentType),
                attachmentHashes: attachments.map(x => x.Hash),
                attachmentSizes: attachments.map(x => x.Size)
            }
        });
        //endregion
    }
}
//endregion

async function run ({ attachmentContentType, attachmentMinSize }) {
    attachmentContentType = attachmentContentType || DEFAULT_ATTACHMENT_CONTENT_TYPE;
    attachmentMinSize = attachmentMinSize || DEFAULT_ATTACHMENT_MIN_SIZE;

    //region Demo
    const session = documentStore.openSession();

    //region Step_4
    const employeesWithMatchingAttachments = await session.query(Employee, Employees_ByAttachmentDetails)
        .whereEquals('attachmentContentTypes', attachmentContentType)
        .whereGreaterThan('attachmentSizes', attachmentMinSize)
        .all();
    //endregion

    //endregion
    return employeesWithMatchingAttachments;
}

module.exports = { run, Employees_ByAttachmentDetails };
