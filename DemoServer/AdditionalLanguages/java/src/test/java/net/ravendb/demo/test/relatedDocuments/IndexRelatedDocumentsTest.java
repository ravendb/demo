package net.ravendb.demo.test.relatedDocuments;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;
import net.ravendb.demo.relatedDocuments.indexRelatedDocuments.IndexRelatedDocuments;
import net.ravendb.demo.staticIndexes.additionalSourcesIndex.AdditionalSourcesIndex;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class IndexRelatedDocumentsTest {

    @Test
    public void test() throws Exception {
        IndexRelatedDocuments.RunParams params = new IndexRelatedDocuments.RunParams();
        params.setCategoryName("Produce");

        DocumentStoreHolder.store.executeIndex(new IndexRelatedDocuments.Products_ByCategoryName());

        List<Product> products = new IndexRelatedDocuments().run(params);

        Assert.assertEquals(5, products.size());
    }
}
