package net.ravendb.demo.test.textSearch;

import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.textSearch.FTSQuerySearchOperators.FTSQuerySearchOperators;
import net.ravendb.demo.textSearch.fTSQuerySearchBasics.FTSQuerySearchBasics;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FTSQuerySearchOperatorsTest {
    @Test
    public void test() {
        FTSQuerySearchOperators.RunParams runParams = new FTSQuerySearchOperators.RunParams();

        runParams.setTerm1("Spanish");
        runParams.setTerm2("Portuguese");
        runParams.setTerm3("Manager");
        List<Employee> employees = new FTSQuerySearchOperators().run(runParams);
        Assert.assertNotNull(employees);
    }
}
