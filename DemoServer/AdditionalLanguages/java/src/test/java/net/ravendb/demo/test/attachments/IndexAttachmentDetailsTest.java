package net.ravendb.demo.test.attachments;

import net.ravendb.demo.attachments.indexAttachmentDetails.IndexAttachmentDetails;
import net.ravendb.demo.common.DocumentStoreHolder;
import org.junit.Test;

public class IndexAttachmentDetailsTest {

    @Test
    public void test() throws Exception {
        DocumentStoreHolder.store.executeIndex(new IndexAttachmentDetails.Employees_ByAttachmentDetails());

        IndexAttachmentDetails.RunParams runParams = new IndexAttachmentDetails.RunParams();

        new IndexAttachmentDetails().run(runParams);
    }
}
