package net.ravendb.demo.test.queries;

import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.queries.filteringResultsMultipleConditions.FilteringResultsMultipleConditions;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FilteringResultsMultipleConditionsTest {
    
    @Test
    public void test() {
        FilteringResultsMultipleConditions.RunParams runParams = new FilteringResultsMultipleConditions.RunParams();
        runParams.setCountry("USA");

        List<Employee> employees = new FilteringResultsMultipleConditions().run(runParams);

        Assert.assertNotNull(employees);
        Assert.assertEquals(3, employees.size());
        Assert.assertEquals("Anne", employees.get(0).getFirstName());
    }
}
