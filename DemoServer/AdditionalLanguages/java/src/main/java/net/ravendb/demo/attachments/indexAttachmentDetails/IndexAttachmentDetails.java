package net.ravendb.demo.attachments.indexAttachmentDetails;

import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import org.apache.commons.lang3.ObjectUtils;

import java.util.List;

public class IndexAttachmentDetails {

    public static class Employees_ByAttachmentDetails extends AbstractIndexCreationTask {
        public static class IndexEntry {
            private String[] attachmentNames;
            private String[] attachmentContentTypes;
            private String[] attachmentHashes;
            private String[] attachmentSizes;

            public String[] getAttachmentNames() {
                return attachmentNames;
            }

            public void setAttachmentNames(String[] attachmentNames) {
                this.attachmentNames = attachmentNames;
            }

            public String[] getAttachmentContentTypes() {
                return attachmentContentTypes;
            }

            public void setAttachmentContentTypes(String[] attachmentContentTypes) {
                this.attachmentContentTypes = attachmentContentTypes;
            }

            public String[] getAttachmentHashes() {
                return attachmentHashes;
            }

            public void setAttachmentHashes(String[] attachmentHashes) {
                this.attachmentHashes = attachmentHashes;
            }

            public String[] getAttachmentSizes() {
                return attachmentSizes;
            }

            public void setAttachmentSizes(String[] attachmentSizes) {
                this.attachmentSizes = attachmentSizes;
            }
        }

        public Employees_ByAttachmentDetails() {
            this.map = "docs.Employees.Select(employee => new {\n" +
                "    employee = employee,\n" +
                "    attachments = this.AttachmentsFor(employee)\n" +
                "}).Select(this0 => new {\n" +
                "    AttachmentNames = Enumerable.ToArray(this0.attachments.Select(x => x.Name)),\n" +
                "    AttachmentContentTypes = Enumerable.ToArray(this0.attachments.Select(x0 => x0.ContentType)),\n" +
                "    AttachmentHashes = Enumerable.ToArray(this0.attachments.Select(x1 => x1.Hash)),\n" +
                "    AttachmentSizes = Enumerable.ToArray(this0.attachments.Select(x2 => x2.Size))\n" +
                "})";
        }
    }

    public List<Employee> run(RunParams runParams) throws Exception {
        String attachmentContentType = ObjectUtils.firstNonNull(runParams.getAttachmentContentType(), "image/jpeg");
        long attachmentMinSize = ObjectUtils.firstNonNull(runParams.getAttachmentMinSize(), 18_000L);

        List<Employee> employeesWithMatchingAttachments;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            employeesWithMatchingAttachments = session.query(Employee.class, Employees_ByAttachmentDetails.class)
                .whereEquals("AttachmentContentTypes", attachmentContentType)
                .whereGreaterThan("AttachmentSizes", attachmentMinSize)
                .toList();
        }

        return employeesWithMatchingAttachments;
    }

    public static class RunParams {
        private String attachmentContentType;
        private Long attachmentMinSize;

        public String getAttachmentContentType() {
            return attachmentContentType;
        }

        public void setAttachmentContentType(String attachmentContentType) {
            this.attachmentContentType = attachmentContentType;
        }

        public Long getAttachmentMinSize() {
            return attachmentMinSize;
        }

        public void setAttachmentMinSize(Long attachmentMinSize) {
            this.attachmentMinSize = attachmentMinSize;
        }
    }
}

