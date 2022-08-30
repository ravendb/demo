package net.ravendb.demo.test.autoIndexes;

import net.ravendb.demo.autoIndexes.autoMapIndex1.AutoMapIndex1;
import net.ravendb.demo.common.models.Employee;
import org.junit.Assert;
import org.junit.Test;

public class AutoMapIndex1Test {

    @Test
    public void test() {
        AutoMapIndex1.RunParams runParams = new AutoMapIndex1.RunParams();
        runParams.setFirstName("Steven");

        Employee employee = new AutoMapIndex1().run(runParams);

        Assert.assertNotNull(employee);
        Assert.assertNotNull(employee.getFirstName());
        Assert.assertEquals("Steven", employee.getFirstName());
    }
}
