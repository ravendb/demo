package net.ravendb.demo.attachments.indexAttachmentDetails;

//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import org.apache.commons.lang3.ObjectUtils;

public class IndexAttachmentDetails {

    //region Demo
    //region Step_1
    public static class Employees_ByAttachmentDetails extends AbstractIndexCreationTask {
    //endregion
        //region Step_2
        public static class IndexEntry {
            private String[] AttachmentNames;
            private String[] AttachmentContentTypes;
            private String[] AttachmentHashes;
            private String[] AttachmentSizes;

            public String[] getAttachmentNames() {
                return AttachmentNames;
            }

            public void setAttachmentNames(String[] attachmentNames) {
                this.AttachmentNames = attachmentNames;
            }

            public String[] getAttachmentContentTypes() {
                return AttachmentContentTypes;
            }

            public void setAttachmentContentTypes(String[] attachmentContentTypes) {
                this.AttachmentContentTypes = attachmentContentTypes;
            }

            public String[] getAttachmentHashes() {
                return AttachmentHashes;
            }

            public void setAttachmentHashes(String[] attachmentHashes) {
                this.AttachmentHashes = attachmentHashes;
            }

            public String[] getAttachmentSizes() {
                return AttachmentSizes;
            }

            public void setAttachmentSizes(String[] attachmentSizes) {
                this.AttachmentSizes = attachmentSizes;
            }
        //endregion
        }

        public Employees_ByAttachmentDetails() {
            //region Step_3
            map = "docs.Employees.Select(employee => new {" +
                "    employee = employee," +
                "    attachments = this.AttachmentsFor(employee)" +
            //endregion
            //region Step_4
                "}).Select(this0 => new {" +
                "    AttachmentNames = Enumerable.ToArray(this0.attachments.Select(x => x.Name))," +
                "    AttachmentContentTypes = Enumerable.ToArray(this0.attachments.Select(x0 => x0.ContentType))," +
                "    AttachmentHashes = Enumerable.ToArray(this0.attachments.Select(x1 => x1.Hash))," +
                "    AttachmentSizes = Enumerable.ToArray(this0.attachments.Select(x2 => x2.Size))" +
                "})";
            //endregion
        }
    }
    //endregion

    public List<Employee> run(RunParams runParams) throws Exception {
        String attachmentContentType = ObjectUtils.firstNonNull(runParams.getAttachmentContentType(), "image/jpeg");
        long attachmentMinSize = ObjectUtils.firstNonNull(runParams.getAttachmentMinSize(), 18_000L);

        //region Demo
        List<Employee> employeesWithMatchingAttachments;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_5
            employeesWithMatchingAttachments = session.query(Employee.class, Employees_ByAttachmentDetails.class)
                .whereEquals("AttachmentContentTypes", attachmentContentType)
                .whereGreaterThan("AttachmentSizes", attachmentMinSize)
                .toList();
            //endregion
        }
        //endregion

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

