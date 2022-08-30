package net.ravendb.demo.test.basics;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.basics.createDocument.CreateDocument;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Company;
import org.junit.Assert;
import org.junit.Test;

public class CreateDocumentTest {
    
    @Test
    public void test() {
        CreateDocument.RunParams runParams = new CreateDocument.RunParams();
        runParams.setCompanyName("companyName1");
        runParams.setCompanyPhone("companyPhone1");
        runParams.setContactName("contactName1");
        runParams.setContactTitle("contactTitle1");

        String newId = new CreateDocument().run(runParams);

        IDocumentSession session = DocumentStoreHolder.store.openSession();
        Company newDocument = session.load(Company.class, newId);

        Assert.assertNotNull(newDocument);
        Assert.assertEquals("companyPhone1", newDocument.getPhone());
    }
}
