package net.ravendb.demo.test.queries;

import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.queries.queryByDocumentId.QueryByDocumentId;
import org.junit.Assert;
import org.junit.Test;

public class QueryByDocumentIdTest {

    @Test
    public void test() {
        QueryByDocumentId.RunParams runParams = new QueryByDocumentId.RunParams();
        runParams.setEmployeeDocumentId("employees/1-A");

        Employee employee = new QueryByDocumentId().run(runParams);

        Assert.assertNotNull(employee);
        Assert.assertNotNull(employee.getLastName());
    }
}
