package net.ravendb.demo.test.staticIndexes;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.staticIndexes.mapReduceIndex.MapReduceIndex;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class MapReduceIndexTest {
    @Test
    public void test() throws Exception {
        MapReduceIndex.RunParams runParams = new MapReduceIndex.RunParams();
        runParams.setCountry("USA");

        DocumentStoreHolder.store.executeIndex(new MapReduceIndex.Employees_ByCountry());

        int numberOfEmployees = new MapReduceIndex().run(runParams);

        // the bellow will fail
        // probably due to bug in store.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        Assert.assertEquals(5, numberOfEmployees);
    }
}
