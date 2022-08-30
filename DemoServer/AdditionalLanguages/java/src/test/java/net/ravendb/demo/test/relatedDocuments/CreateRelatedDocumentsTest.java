package net.ravendb.demo.test.relatedDocuments;

import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;
import net.ravendb.demo.relatedDocuments.createRelatedDocuments.CreateRelatedDocuments;
import org.junit.Assert;
import org.junit.Test;

public class CreateRelatedDocumentsTest {

    @Test
    public void test() throws Exception {
        CreateRelatedDocuments.RunParams params = new CreateRelatedDocuments.RunParams();
        params.setProductName("p1");
        params.setSupplierName("s2");
        params.setSupplierPhone("123");

        Product product = new CreateRelatedDocuments().run(params);

        IDocumentSession session = DocumentStoreHolder.store.openSession();
        Product loadedDocument = session.load(Product.class, product.getId());

        Assert.assertNotNull(loadedDocument.getSupplier());
        Assert.assertNotNull(loadedDocument.getCategory());
    }
}
