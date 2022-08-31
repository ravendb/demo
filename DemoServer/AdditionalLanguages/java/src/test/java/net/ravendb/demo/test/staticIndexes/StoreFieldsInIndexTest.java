package net.ravendb.demo.test.staticIndexes;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.staticIndexes.storeFieldInIndex.StoreFieldsInIndex;
import net.ravendb.demo.test.util.TestUtils;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class StoreFieldsInIndexTest {

    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new StoreFieldsInIndex.OrdersQuantity_ByCompany());
        TestUtils.waitForIndexing(DocumentStoreHolder.store);

        StoreFieldsInIndex.RunParams runParams = new StoreFieldsInIndex.RunParams();
        runParams.setCompanyId("companies/1-A");

        List<StoreFieldsInIndex.OrdersQuantity_ByCompany.OrderProjectedDetails> orders = new StoreFieldsInIndex().run(runParams);

        Assert.assertEquals(6, orders.size());
        Assert.assertNotNull(orders.get(0).getOrderedAt());
    }
}
