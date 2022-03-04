package net.ravendb.demo.basics.theSession;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;

public class TheSession {

    public void run() throws Exception {
        //region Demo
        //region Step_1
        IDocumentSession session = DocumentStoreHolder.store.openSession("YourDatabaseName");
        //endregion

        //region Step_2
        //   Run your business logic:
        //
        //   Store documents
        //   Load and Modify documents
        //   Query indexes & collections
        //   Delete document
        //   .... etc.
        //endregion

        //region Step_3
        session.saveChanges();
        //endregion
        //endregion
    }
}
