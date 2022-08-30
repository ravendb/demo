package net.ravendb.demo.test.queries;

import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.queries.queryExample.QueryExample;
import org.junit.Assert;
import org.junit.Test;
import java.util.List;

public class QueryExampleTest {
    
    @Test
    public void test() {
        List<Employee> employees = new QueryExample().run();

        Assert.assertNotNull(employees);
        Assert.assertEquals(5, employees.size());
        Assert.assertNotNull(employees.get(0).getFirstName());
    }
}
