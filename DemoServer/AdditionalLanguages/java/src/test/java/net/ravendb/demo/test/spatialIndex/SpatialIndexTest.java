package net.ravendb.demo.test.spatialIndex;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Company;
import net.ravendb.demo.spatial.spatialIndex.SpatialIndex;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class SpatialIndexTest {

    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new SpatialIndex.Companies_ByLocation());

        List<Company> companies = new SpatialIndex().run();

        Assert.assertEquals(4, companies.size());
        Assert.assertNotNull(companies.get(0).getName());
    }
}
