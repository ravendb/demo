package net.ravendb.demo.test.staticIndexes;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.staticIndexes.mapIndex.MapIndex;
import net.ravendb.demo.test.util.TestUtils;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class MapIndexTest {
    @Test
    public void test() throws Exception {
        MapIndex.RunParams runParams = new MapIndex.RunParams();
        runParams.setStartYear(1993);

        DocumentStoreHolder.store.executeIndex(new MapIndex.Employees_ImportantDetails());
        TestUtils.waitForIndexing(DocumentStoreHolder.store);

        List<Employee> employees = new MapIndex().run(runParams);

        Assert.assertEquals(1, employees.size());
        Assert.assertEquals("employees/8-A", employees.get(0).getId());
    }
}
