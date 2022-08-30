package net.ravendb.demo.test.spatialIndex;

import net.ravendb.demo.spatial.spatialQuery.SpatialQuery;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class SpatialQueryTest {

    @Test
    public void test() {
        SpatialQuery.RunParams runParams = new SpatialQuery.RunParams();
        runParams.setRadius(2);

        List<SpatialQuery.EmployeeDetails> results = new SpatialQuery().run(runParams);

        Assert.assertEquals(2, results.size());
        Assert.assertNotNull(results.get(0).getEmployeeName());
    }
}
