package net.ravendb.demo.test.textSearch;

import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.textSearch.highlightQueryResultsCustomized.HighlightQueryResultsCustomized;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class HighlightQueryResultsCustomizedTest {

    @Test
    public void test() {

        DocumentStoreHolder.store.executeIndex(new HighlightQueryResultsCustomized.EmployeesDetails());

        HighlightQueryResultsCustomized.RunParams runParams = new HighlightQueryResultsCustomized.RunParams();
        List<HighlightQueryResultsCustomized.DataToShow> results = new HighlightQueryResultsCustomized().run(runParams);
        Assert.assertNotNull(results);
    }
}
