package net.ravendb.demo.basics.theSession;

//region Usings
import net.ravendb.client.documents.IDocumentStore;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;

public class TheSession {
    //region Demo
    //region Step_1
    IDocumentSession session = DocumentStoreHolder.getStore().openSession("YourDatabaseName");
    //endregion

    //region Step_2
    //   Run your business logic:
    //
    //   Store documents
    //   Load and Modify documents
    //   Query indexes & collections
    //   Delete docum
    //endregion

    //region Step_3
    session.saveChanges();
    //endregion
    //endregion
}
