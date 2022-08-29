package net.ravendb.demo.test.multiMapIndexes;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.multiMapIndexes.multiMapReduceIndex.MultiMapReduceIndex;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class MultiMapReduceIndexTest {
    @Test
    public void test() throws Exception {
        DocumentStoreHolder.store.executeIndex(new MultiMapReduceIndex.CityCommerceDetails());

        List<MultiMapReduceIndex.CityCommerceDetails.IndexEntry> results = new MultiMapReduceIndex().run(new MultiMapReduceIndex.RunParams());

        // The properties in the documents returned are null - probably due to bug with
        // store.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        Assert.assertEquals(4, results.size());
    }
}
