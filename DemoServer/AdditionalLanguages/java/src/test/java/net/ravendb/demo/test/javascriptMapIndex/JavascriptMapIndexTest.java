package net.ravendb.demo.test.javascriptMapIndex;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.javascriptIndexes.javascriptMapIndex.JavascriptMapIndex;
import net.ravendb.demo.test.util.TestUtils;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class JavascriptMapIndexTest {
    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new JavascriptMapIndex.Employees_ByImportantDetailsJS());
        TestUtils.waitForIndexing(DocumentStoreHolder.store);

        JavascriptMapIndex.RunParams runParams = new JavascriptMapIndex.RunParams();
        runParams.setStartYear(1993);

        List<Employee> employees = new JavascriptMapIndex().run(runParams);

        Assert.assertEquals(1, employees.size());
        Assert.assertNotNull(employees.get(0).getFirstName());
    }
}
