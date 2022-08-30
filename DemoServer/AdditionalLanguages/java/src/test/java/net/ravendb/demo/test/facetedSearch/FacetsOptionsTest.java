package net.ravendb.demo.test.facetedSearch;

import net.ravendb.client.documents.queries.facets.FacetResult;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.facetedSearch.facetsOptions.FacetsOptions;
import org.junit.Assert;
import org.junit.Test;

import java.util.Map;

public class FacetsOptionsTest {
    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new FacetsOptions.Products_ByCategoryAndSupplier());

        FacetsOptions.RunParams runParams = new FacetsOptions.RunParams();
        Map<String, FacetResult> results = new FacetsOptions().run(runParams);

        Assert.assertNotNull(results);
        Assert.assertEquals(2, results.size());
        Assert.assertEquals(64, results.get("supplier").getRemainingHits());
    }
}
