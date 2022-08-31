package net.ravendb.demo.test.staticIndexes;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.staticIndexes.additionalSourcesIndex.AdditionalSourcesIndex;
import net.ravendb.demo.test.util.TestUtils;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class AdditionalSourcesIndexTest {

    @Test
    public void test() throws Exception {
        DocumentStoreHolder.store.executeIndex(new AdditionalSourcesIndex.Products_ByPrice());
        TestUtils.waitForIndexing(DocumentStoreHolder.store);

        List<AdditionalSourcesIndex.DataToShow> results = new AdditionalSourcesIndex().run(new AdditionalSourcesIndex.RunParams());

        Assert.assertEquals(3, results.size());
        Assert.assertEquals(2.5, results.get(0).getOriginalPrice(), 0);
    }
}
