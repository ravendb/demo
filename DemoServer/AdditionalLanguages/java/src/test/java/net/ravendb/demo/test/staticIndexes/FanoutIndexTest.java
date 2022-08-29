package net.ravendb.demo.test.staticIndexes;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Order;
import net.ravendb.demo.staticIndexes.fanoutIndex.FanoutIndex;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FanoutIndexTest {
    @Test
    public void test() throws Exception {
        FanoutIndex.RunParams runParams = new FanoutIndex.RunParams();
        runParams.setNamePrefix("Chocolade");

        DocumentStoreHolder.store.executeIndex(new FanoutIndex.Orders_ByProductDetails());

        List<Order> orders = new FanoutIndex().run(runParams);

        Assert.assertEquals(6, orders.size());
    }
}
