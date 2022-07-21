package net.ravendb.demo.test.spatialIndex;

import net.ravendb.demo.spatial.spatialQuery.SpatialQuery;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class SpatialQueryTest {

    @Test
    public void test() {
        SpatialQuery.RunParams runParams = new SpatialQuery.RunParams();
        List<SpatialQuery.EmployeeDetails> results = new SpatialQuery().run(runParams);
        Assert.assertNotNull(results);
    }
}
