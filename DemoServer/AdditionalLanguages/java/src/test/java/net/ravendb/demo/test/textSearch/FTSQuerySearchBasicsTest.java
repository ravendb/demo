package net.ravendb.demo.test.textSearch;

import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.staticIndexes.storeFieldInIndex.StoreFieldsInIndex;
import net.ravendb.demo.textSearch.fTSQuerySearchBasics.FTSQuerySearchBasics;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FTSQuerySearchBasicsTest {

    @Test
    public void test() {
        FTSQuerySearchBasics.RunParams runParams = new FTSQuerySearchBasics.RunParams();

        runParams.setTerm1("Washington");
        runParams.setTerm2("Colorado");
        List<Employee> employees = new FTSQuerySearchBasics().run(runParams);
        Assert.assertNotNull(employees);
    }
}
