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
        private String documentId;

        public String getDocumentId() {
            return documentId;
        }

        public void setDocumentId(String documentId) {
            documentId = documentId;
        }
    }
}
