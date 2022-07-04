package net.ravendb.demo.test.textSearch;

import net.ravendb.demo.common.models.Employee;

import net.ravendb.demo.textSearch.highlightQueryResultsBasics.HighlightQueryResultsBasics;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class HighlightQueryResultsBasicsTest {
    @Test
    public void test() {
        HighlightQueryResultsBasics.RunParams runParams = new HighlightQueryResultsBasics.RunParams();

        runParams.setFragmentLength(50);
        runParams.setFragmentCount(2);
        List<Employee> employees = new HighlightQueryResultsBasics().run(runParams);
        Assert.assertNotNull(employees);
    }
}
