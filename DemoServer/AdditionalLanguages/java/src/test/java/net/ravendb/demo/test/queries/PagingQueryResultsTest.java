package net.ravendb.demo.test.queries;

import net.ravendb.demo.common.models.Company;
import net.ravendb.demo.queries.pagingQueryResults.PagingQueryResults;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class PagingQueryResultsTest {
    
    @Test
    public void test() {
        PagingQueryResults.RunParams runParams = new PagingQueryResults.RunParams();
        runParams.setResultsToSkip(10);
        runParams.setResultsToTake(5);

        List<Company> companies = new PagingQueryResults().run(runParams);

        Assert.assertNotNull(companies);
        Assert.assertEquals(5, companies.size());
        Assert.assertNotNull(companies.get(0).getName());
    }
}
