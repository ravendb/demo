package net.ravendb.demo.test.facetedSearch;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.facetedSearch.facetsBasics.FacetsBasics;
import net.ravendb.demo.test.util.TestUtils;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FacetsBasicTest {

    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new FacetsBasics.Products_ByCategoryAndPrice());
        TestUtils.waitForIndexing(DocumentStoreHolder.store);

        FacetsBasics.RunParams runParams = new FacetsBasics.RunParams();
        List<FacetsBasics.MyFacetResult> facetResults = new FacetsBasics().run(runParams);

        Assert.assertNotNull(facetResults);
        Assert.assertEquals(facetResults.size(), 12);
        Assert.assertEquals("Price per Unit", facetResults.get(0).getFacetName());
    }
}
