package net.ravendb.demo.test.staticIndexes;

import net.ravendb.demo.staticIndexes.projectIndexResults.ProjectIndexResults;
import org.junit.Assert;
import org.junit.Test;

import java.util.List;

public class ProjectIndexResultsTest {
    int year = 1993;
    @Test
    public void test() {
        ProjectIndexResults.RunParams params = new ProjectIndexResults.RunParams();
        params.setStartYear(1993);

        List<ProjectIndexResults.Employees_ByWorkPeriod.EmployeeProjectedDetails> result =
            new ProjectIndexResults().run(params);

        // The properties in the documents returned are null due to bug when using selectFields() together with
        // store.getConventions().getEntityMapper().setPropertyNamingStrategy(new JsonExtensions.DotNetNamingStrategy());
        Assert.assertEquals(3, result.size());
    }
}
