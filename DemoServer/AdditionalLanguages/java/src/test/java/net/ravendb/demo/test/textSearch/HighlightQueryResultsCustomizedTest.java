package net.ravendb.demo.test.textSearch;

import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.textSearch.highlightQueryResultsCustomized.HighlightQueryResultsCustomized;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class HighlightQueryResultsCustomizedTest {
    @Test
    public void test() {
        HighlightQueryResultsCustomized.RunParams runParams = new HighlightQueryResultsCustomized.RunParams();

        runParams.setFragmentLength(100);
        runParams.setFragmentCount(1);
        runParams.setTag1("+++");
        runParams.setTag2("+++");
        runParams.setTag3("<<<");
        runParams.setTag4(">>>");
        List<Employee> employees = new HighlightQueryResultsCustomized().run(runParams);
        Assert.assertNotNull(employees);
    }
}
