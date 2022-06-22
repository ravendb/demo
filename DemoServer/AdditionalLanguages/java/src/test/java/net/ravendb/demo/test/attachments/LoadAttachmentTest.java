package net.ravendb.demo.test.attachments;

import net.ravendb.demo.attachments.loadAttachment.LoadAttachment;
import org.junit.Test;

public class LoadAttachmentTest {

    @Test
    public void test() throws Exception {
        LoadAttachment.RunParams params = new LoadAttachment.RunParams();
        params.setAttachmentName("image.");

        new LoadAttachment().run(params);
    }
}
