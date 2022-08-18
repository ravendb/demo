package net.ravendb.demo.test.facetedSearch;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.facetedSearch.facetsBasics.FacetsBasics;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FacetsBasicTest {

    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new FacetsBasics.Products_ByCategoryAndPrice());

        FacetsBasics.RunParams runParams = new FacetsBasics.RunParams();
        List<FacetsBasics.MyFacetResult> facetResults = new FacetsBasics().run(runParams);
        Assert.assertNotNull(facetResults);
        Assert.assertEquals(facetResults.size(), 12);
    }
}
