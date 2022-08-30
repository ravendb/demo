package net.ravendb.demo.test.queries;

import net.ravendb.demo.common.models.Company;
import net.ravendb.demo.queries.fullCollectionQuery.FullCollectionQuery;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FullCollectionQueryTest {
    
    @Test
    public void test() {
        List<Company> companies = new FullCollectionQuery().run();

        Assert.assertNotNull(companies);
        Assert.assertEquals(91, companies.size());
        Assert.assertNotNull(companies.get(0).getName());
    }
}
