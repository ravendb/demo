package net.ravendb.demo.attachments.storeAttachment;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import org.apache.commons.lang3.ObjectUtils;

import java.io.ByteArrayInputStream;
import java.io.InputStream;

public class StoreAttachment {
    private static final String DEFAULT_DOCUMENT_ID = "companies/2-A";

    public void run(RunParams runParams) throws Exception {
        String documentId = ObjectUtils.firstNonNull(runParams.getDocumentId(), DEFAULT_DOCUMENT_ID);

        String attachmentName = runParams.getAttachmentName();
        String contentType = runParams.getContentType();
        byte[] attachment = runParams.getAttachment();

        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            try (InputStream stream = new ByteArrayInputStream(attachment)) {
            //endregion

                //region Step_2
                session.advanced().attachments().store(documentId, attachmentName, stream, contentType);
                //endregion

                //region Step_3
                session.saveChanges();
                //endregion
            }
        }
        //endregion
    }

    public static class RunParams {
        private String documentId;
        private byte[] attachment;
        private String attachmentName;
        private String contentType;

        public String getDocumentId() {
            return documentId;
        }

        public void setDocumentId(String documentId) {
            this.documentId = documentId;
        }

        public byte[] getAttachment() {
            return attachment;
        }

        public void setAttachment(byte[] attachment) {
            this.attachment = attachment;
        }

        public String getAttachmentName() {
            return attachmentName;
        }

        public void setAttachmentName(String attachmentName) {
            this.attachmentName = attachmentName;
        }

        public String getContentType() {
            return contentType;
        }

        public void setContentType(String contentType) {
            this.contentType = contentType;
        }
    }
}
