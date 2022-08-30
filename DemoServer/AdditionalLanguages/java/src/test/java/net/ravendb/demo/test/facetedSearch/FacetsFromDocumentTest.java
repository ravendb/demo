package net.ravendb.demo.test.facetedSearch;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.facetedSearch.facetsFromDocument.FacetsFromDocument;
import org.junit.Assert;
import org.junit.Test;
import java.util.List;

public class FacetsFromDocumentTest {

    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new FacetsFromDocument.Products_ByCategoryAndPrice());

        FacetsFromDocument.RunParams runParams = new FacetsFromDocument.RunParams();
        List<FacetsFromDocument.MyFacetResult> results = new FacetsFromDocument().run(runParams);

        Assert.assertNotNull(results);
        Assert.assertEquals(results.size(), 12);
        Assert.assertEquals("beverages", results.get(0).getFacetRange());
    }
}
