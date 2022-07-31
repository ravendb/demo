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

        runParams.setFragmentLength(100);
        runParams.setFragmentCount(1);
        runParams.setTag1("+++");
        runParams.setTag2("+++");
        runParams.setTag3("<<<");
        runParams.setTag4(">>>");

        List<HighlightQueryResultsCustomized.DataToShow> results = new HighlightQueryResultsCustomized().run(runParams);
        Assert.assertTrue(results.size() == 5);
    }
}
