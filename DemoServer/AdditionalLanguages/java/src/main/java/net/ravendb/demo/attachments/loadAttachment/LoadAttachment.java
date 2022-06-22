package net.ravendb.demo.attachments.loadAttachment;

import net.ravendb.client.documents.operations.attachments.CloseableAttachmentResult;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.attachments.storeAttachment.StoreAttachment;
import net.ravendb.demo.common.DocumentStoreHolder;
import org.apache.commons.io.IOUtils;
import org.apache.commons.lang3.ObjectUtils;

import java.io.ByteArrayOutputStream;

public class LoadAttachment {

    private static final String DEFAULT_DOCUMENT_ID = "categories/1-A";

    public void run(RunParams runParams) throws Exception {
        String documentId = ObjectUtils.firstNonNull(runParams.getDocumentId(), DEFAULT_DOCUMENT_ID);

        String attachmentName = runParams.getAttachmentName();
        boolean attachmentExists;

        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            attachmentExists = session.advanced().attachments().exists(documentId, attachmentName);
            //endregion

            if (attachmentExists) {
                //region Step_2
                try (CloseableAttachmentResult attachmentResult = session.advanced().attachments().get(documentId, attachmentName)) {

                    //region Step_3
                    String contentType = attachmentResult.getDetails().getContentType();
                    String hash = attachmentResult.getDetails().getHash();
                    long size = attachmentResult.getDetails().getSize();
                    //endregion

                    //region Step_4
                    ByteArrayOutputStream attachmentMemoryStream = new ByteArrayOutputStream();
                    IOUtils.copy(attachmentResult.getData(), attachmentMemoryStream);
                    byte[] bytes = attachmentMemoryStream.toByteArray();
                    //endregion
                }
                //endregion
            }
        }
        //endregion
    }

    public static class RunParams {
        private String documentId;
        private String attachmentName;

        public String getDocumentId() {
            return documentId;
        }

        public void setDocumentId(String documentId) {
            this.documentId = documentId;
        }

        public String getAttachmentName() {
            return attachmentName;
        }

        public void setAttachmentName(String attachmentName) {
            this.attachmentName = attachmentName;
        }
    }
}
