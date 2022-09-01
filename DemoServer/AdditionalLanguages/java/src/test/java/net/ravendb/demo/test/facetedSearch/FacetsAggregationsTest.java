package net.ravendb.demo.test.facetedSearch;

import net.ravendb.client.documents.queries.facets.FacetResult;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.facetedSearch.facetsAggregations.FacetsAggregations;
import net.ravendb.demo.test.util.TestUtils;
import org.junit.Assert;
import org.junit.Test;

import java.util.Map;

public class FacetsAggregationsTest {

    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new FacetsAggregations.Products_ByCategoryPriceAndUnits());
        TestUtils.waitForIndexing(DocumentStoreHolder.store);

        FacetsAggregations.RunParams runParams = new FacetsAggregations.RunParams();
        Map<String, FacetResult> results = new FacetsAggregations().run(runParams);

        Assert.assertNotNull(results);
        Assert.assertEquals(2, results.size());
        Assert.assertNotNull(results.get("CategoryName"));
        Assert.assertEquals(16, results.get("CategoryName").getValues().size());
    }
}
