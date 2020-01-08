package net.ravendb.demo.test.relatedDocuments;

import net.ravendb.demo.common.models.Company;
import net.ravendb.demo.relatedDocuments.createRelatedDocuments.CreateRelatedDocuments;
import org.junit.Test;

public class CreateRelatedDocumentsTest {

    @Test
    public void test() throws Exception {
        CreateRelatedDocuments.RunParams params = new CreateRelatedDocuments.RunParams();
        params.setProductName("p1");
        params.setSupplierName("s2");
        params.setSupplierPhone("123");

        new CreateRelatedDocuments().run(params);
    }

}
