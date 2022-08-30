package net.ravendb.demo.test.autoIndexes;

import net.ravendb.demo.autoIndexes.autoMapIndex2.AutoMapIndex2;
import net.ravendb.demo.common.models.Employee;
import org.junit.Assert;
import org.junit.Test;

public class AutoMapIndex2Test {

    @Test
    public void test() {
        AutoMapIndex2.RunParams runParams = new AutoMapIndex2.RunParams();
        runParams.setCountry("UK");

        Employee employee = new AutoMapIndex2().run(runParams);

        Assert.assertNotNull(employee);
        Assert.assertNotNull(employee.getFirstName());
        Assert.assertEquals("Anne", employee.getFirstName());
    }
}
