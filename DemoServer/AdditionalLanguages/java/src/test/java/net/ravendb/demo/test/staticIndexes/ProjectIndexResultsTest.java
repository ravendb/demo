package net.ravendb.demo.test.staticIndexes;

import net.ravendb.demo.staticIndexes.projectIndexResults.ProjectIndexResults;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class ProjectIndexResultsTest {
    @Test
    public void test() {
        ProjectIndexResults.RunParams params = new ProjectIndexResults.RunParams();
        params.setStartYear(1993);

        List<ProjectIndexResults.Employees_ByWorkPeriod.EmployeeProjectedDetails> result = new ProjectIndexResults().run(params);

        Assert.assertEquals(3, result.size());
        Assert.assertNotNull(result.get(0).getFirstName());
    }
}
