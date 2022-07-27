package net.ravendb.demo.test.facetedSearch;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.facetedSearch.facetsBasics.FacetsBasic;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FacetsBasicTest {

    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new FacetsBasic.Products_ByCategoryAndPrice());

        FacetsBasic.RunParams runParams = new FacetsBasic.RunParams();
        List<FacetsBasic.MyFacetResult> facetResults = new FacetsBasic().run(runParams);
        Assert.assertNotNull(facetResults);
    }
}
