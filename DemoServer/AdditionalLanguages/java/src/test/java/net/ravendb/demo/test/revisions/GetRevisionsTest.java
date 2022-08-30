package net.ravendb.demo.test.revisions;

import net.ravendb.demo.common.models.Company;
import net.ravendb.demo.revisions.getRevisions.GetRevisions;
import org.junit.Assert;
import org.junit.Test;
import java.util.List;

public class GetRevisionsTest {
    
    @Test
    public void test() {
        List<Company> companies = new GetRevisions().run();

        Assert.assertNotNull(companies);
    }
}
