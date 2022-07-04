package net.ravendb.demo.test.staticIndexes;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.staticIndexes.additionalSourcesIndex.AdditionalSourcesIndex;
import org.junit.Test;

public class AdditionalSourcesIndexTest {

    @Test
    public void test() throws Exception {

        DocumentStoreHolder.store.executeIndex(new AdditionalSourcesIndex.Products_ByPrice());

        new AdditionalSourcesIndex().run(new AdditionalSourcesIndex.RunParams());
    }
}
