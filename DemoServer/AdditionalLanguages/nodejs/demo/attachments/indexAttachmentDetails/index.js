const { documentStore } = require('../../common/docStoreHolder');
const { Employee } = require('../../common/models');
const { AbstractCsharpIndexCreationTask } = require('ravendb');

const DEFAULT_ATTACHMENT_CONTENT_TYPE = 'image/jpeg';
const DEFAULT_ATTACHMENT_MIN_SIZE = 18000;

class Employees_ByAttachmentDetails extends AbstractCsharpIndexCreationTask {
    constructor () {
        super();

        this.map = 'docs.Employees.Select(employee => new {' +
            '    employee = employee,' +
            '    attachments = this.AttachmentsFor(employee)' +
            '}).Select(this0 => new {' +
            '    attachmentNames = Enumerable.ToArray(this0.attachments.Select(x => x.Name)),' +
            '    attachmentContentTypes = Enumerable.ToArray(this0.attachments.Select(x0 => x0.ContentType)),' +
            '    attachmentHashes = Enumerable.ToArray(this0.attachments.Select(x1 => x1.Hash)),' +
            '    attachmentSizes = Enumerable.ToArray(this0.attachments.Select(x2 => x2.Size))' +
            '})';
    }
}

async function run ({ attachmentContentType, attachmentMinSize }) {
    attachmentContentType = attachmentContentType || DEFAULT_ATTACHMENT_CONTENT_TYPE;
    attachmentMinSize = attachmentMinSize || DEFAULT_ATTACHMENT_MIN_SIZE;

    const session = documentStore.openSession();

    const employeesWithMatchingAttachments = await session.query(Employee, Employees_ByAttachmentDetails)
        .whereEquals('attachmentContentTypes', attachmentContentType)
        .whereGreaterThan('attachmentSizes', attachmentMinSize)
        .all();

    return employeesWithMatchingAttachments;
}

module.exports = { run, Employees_ByAttachmentDetails };
