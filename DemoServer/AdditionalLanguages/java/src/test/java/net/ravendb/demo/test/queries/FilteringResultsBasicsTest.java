package net.ravendb.demo.test.queries;

import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.queries.filteringResultsBasics.FilteringResultsBasics;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FilteringResultsBasicsTest {
    
    @Test
    public void test() {
        List<Employee> employees = new FilteringResultsBasics().run();

        Assert.assertNotNull(employees);
        Assert.assertEquals(1, employees.size());
        Assert.assertEquals("Anne", employees.get(0).getFirstName());
    }
}
