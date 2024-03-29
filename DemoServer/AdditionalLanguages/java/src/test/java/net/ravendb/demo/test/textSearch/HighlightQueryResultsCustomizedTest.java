package net.ravendb.demo.test.textSearch;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.test.util.TestUtils;
import net.ravendb.demo.textSearch.highlightQueryResultsCustomized.HighlightQueryResultsCustomized;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class HighlightQueryResultsCustomizedTest {

    @Test
    public void test() {
        DocumentStoreHolder.store.executeIndex(new HighlightQueryResultsCustomized.EmployeesDetails());
        TestUtils.waitForIndexing(DocumentStoreHolder.store);

        HighlightQueryResultsCustomized.RunParams runParams = new HighlightQueryResultsCustomized.RunParams();
        List<HighlightQueryResultsCustomized.DataToShow> results = new HighlightQueryResultsCustomized().run(runParams);

        Assert.assertEquals(5, results.size());
        Assert.assertEquals("employees/5-A", results.get(0).getDocumentId());
    }
}
