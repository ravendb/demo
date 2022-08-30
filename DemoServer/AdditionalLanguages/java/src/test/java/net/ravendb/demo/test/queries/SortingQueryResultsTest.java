package net.ravendb.demo.test.queries;

import net.ravendb.demo.common.models.Product;
import net.ravendb.demo.queries.sortingQueryResults.SortingQueryResults;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class SortingQueryResultsTest {
    
    @Test
    public void test() {
        SortingQueryResults.RunParams runParams = new SortingQueryResults.RunParams();
        runParams.setNumberOfUnits(20);

        List<Product> products = new SortingQueryResults().run(runParams);

        Assert.assertNotNull(products);
        Assert.assertEquals(19, products.size());
        Assert.assertNotNull(products.get(0).getName());
    }
}
