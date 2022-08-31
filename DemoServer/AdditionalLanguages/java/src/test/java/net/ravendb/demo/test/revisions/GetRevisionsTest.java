package net.ravendb.demo.test.revisions;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Company;
import net.ravendb.demo.revisions.getRevisions.GetRevisions;
import org.junit.Assert;
import org.junit.Test;
import java.util.List;

public class GetRevisionsTest {
    
    @Test
    public void test() {
        String documentId = "companies/7-A";

        IDocumentSession session = DocumentStoreHolder.store.openSession();
        Company documentToRestore = session.load(Company.class, documentId);

        List<Company> companies = new GetRevisions().run();

        try {
            Assert.assertNotNull(companies);
        } finally {
            IDocumentSession cleanupSession = DocumentStoreHolder.store.openSession();
            cleanupSession.delete(documentId);
            cleanupSession.saveChanges();

            cleanupSession.store(documentToRestore);
            cleanupSession.saveChanges();
        }
    }
}
