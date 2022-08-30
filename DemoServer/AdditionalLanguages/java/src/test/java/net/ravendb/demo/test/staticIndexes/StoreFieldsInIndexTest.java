package net.ravendb.demo.test.staticIndexes;

import net.ravendb.demo.staticIndexes.storeFieldInIndex.StoreFieldsInIndex;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class StoreFieldsInIndexTest {

    @Test
    public void test() {
        StoreFieldsInIndex.RunParams runParams = new StoreFieldsInIndex.RunParams();
        runParams.setCompanyId("companies/1-A");

        List<StoreFieldsInIndex.OrdersQuantity_ByCompany.OrderProjectedDetails> orders = new StoreFieldsInIndex().run(runParams);

        Assert.assertEquals(6, orders.size());
        Assert.assertNotNull(orders.get(0).getOrderedAt()); //TODO: this fails
    }
}
