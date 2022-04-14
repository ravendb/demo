package net.ravendb.demo.test.textSearch;

import net.ravendb.demo.common.models.Employee;

import net.ravendb.demo.textSearch.fTSQuerySearchBasics.FTSQuerySearchBoosting;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class FTSQuerySearchOperatorsTest {
    @Test
    public void test() {
        FTSQuerySearchBoosting.RunParams runParams = new FTSQuerySearchBoosting.RunParams();
        runParams.setBoost1(100);
        runParams.setBoost2(20);
        runParams.setBoost3(5);
        List<Employee> employees = new FTSQuerySearchBoosting().run(runParams);
        Assert.assertNotNull(employees);
    }
}
