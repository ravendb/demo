package net.ravendb.demo.test.staticIndexes;

import net.ravendb.client.documents.DocumentStore;
import net.ravendb.demo.autoIndexes.autoMapReduceIndex.AutoMapReduceIndex;
import net.ravendb.demo.relatedDocuments.createRelatedDocuments.CreateRelatedDocuments;
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

        Assert.assertNotNull(result);
    }
}
