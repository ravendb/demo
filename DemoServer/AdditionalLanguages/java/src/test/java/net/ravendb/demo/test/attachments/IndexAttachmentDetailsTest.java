package net.ravendb.demo.test.attachments;

import net.ravendb.demo.attachments.indexAttachmentDetails.IndexAttachmentDetails;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.test.util.TestUtils;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class IndexAttachmentDetailsTest {

    @Test
    public void test() throws Exception {
        DocumentStoreHolder.store.executeIndex(new IndexAttachmentDetails.Employees_ByAttachmentDetails());
        TestUtils.waitForIndexing(DocumentStoreHolder.store);

        IndexAttachmentDetails.RunParams runParams = new IndexAttachmentDetails.RunParams();

        List<Employee> employees = new IndexAttachmentDetails().run(runParams);

        Assert.assertEquals(2, employees.size());
        Assert.assertNotNull(employees.get(0).getLastName());
    }
}
