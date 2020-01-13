package net.ravendb.demo.queries.fullCollectionQuery;
//region Usings
import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Company;

public class FullCollectionQuery {
    public void run() {
        //region Demo
        List<Company> collectionResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            IDocumentQuery<Company> fullCollectionQuery = session.query(Company.class);
            //endregion

            //region Step_2
            collectionResults = fullCollectionQuery.toList();
            //endregion
        }
        //endregion
    }

}
