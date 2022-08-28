package net.ravendb.demo.test.staticIndexes;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.staticIndexes.mapIndex.MapIndex;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class MapIndexTest {
    @Test
    public void test() throws Exception {
        MapIndex.RunParams runParams = new MapIndex.RunParams();
        runParams.setStartYear(1993);

        DocumentStoreHolder.store.executeIndex(new MapIndex.Employees_ImportantDetails());

        List<Employee> employees = new MapIndex().run(runParams);

        Assert.assertEquals(1, employees.size());
    }
}
