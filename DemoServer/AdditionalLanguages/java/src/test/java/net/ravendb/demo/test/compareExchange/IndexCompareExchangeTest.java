package net.ravendb.demo.test.compareExchange;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Product;
import net.ravendb.demo.compareExchange.indexCompareExchange.IndexCompareExchange;
import net.ravendb.demo.test.util.TestUtils;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class IndexCompareExchangeTest {
    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new IndexCompareExchange.Products_ByUnitsInStock());
        TestUtils.waitForIndexing(DocumentStoreHolder.store);

        IndexCompareExchange.RunParams runParams = new IndexCompareExchange.RunParams();
        runParams.setMinValue(25);

        List<Product> products = new IndexCompareExchange().run(runParams);

        Assert.assertEquals(7, products.size());
        Assert.assertNotNull(products.get(0).getName());
    }
}
