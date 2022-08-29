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

        // The properties in the documents returned are null due to bug when using selectFields() together with
        // store.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        Assert.assertEquals(6, orders.size());
    }
}
