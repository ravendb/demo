package net.ravendb.demo.basics.deleteDocument;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;

public class DeleteDocument {

    public void run(RunParams runParams) {
        String documentID = runParams.getDocumentId();

        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            session.delete(documentID);
            //endregion

            //region Step_2
            session.saveChanges();
            //endregion
        }
        //endregion
    }

    public static class RunParams {
        private String DocumentId;

        public String getDocumentId() {
            return DocumentId;
        }

        public void setDocumentId(String documentId) {
            DocumentId = documentId;
        }
    }
}
