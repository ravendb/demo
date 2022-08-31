package net.ravendb.demo.test.multiMapIndexes;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.multiMapIndexes.multiMapIndexBasic.MultiMapIndexBasic;
import net.ravendb.demo.test.util.TestUtils;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class MultiMapIndexBasicTest {

    @Test
    public void test() throws Exception {
        DocumentStoreHolder.store.executeIndex(new MultiMapIndexBasic.CompaniesAndSuppliers_ByName());
        TestUtils.waitForIndexing(DocumentStoreHolder.store);

        List<MultiMapIndexBasic.CompaniesAndSuppliers_ByName.IndexEntry> results = new MultiMapIndexBasic().run(new MultiMapIndexBasic.RunParams());

        Assert.assertEquals(5, results.size());
        Assert.assertNotNull(results.get(0).getName());
    }
}
