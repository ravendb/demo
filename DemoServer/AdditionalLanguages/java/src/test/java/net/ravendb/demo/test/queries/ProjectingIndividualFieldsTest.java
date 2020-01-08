package net.ravendb.demo.test.queries;

import net.ravendb.demo.queries.projectingIndividualFields.ProjectingIndividualFields;
import org.junit.Test;

import java.util.List;

import static org.junit.Assert.assertNotNull;

public class ProjectingIndividualFieldsTest {
    
    @Test
    public void test() {
        List<ProjectingIndividualFields.CompanyDetails> run = new ProjectingIndividualFields().run();
        assertNotNull(run);
        assertNotNull(run.get(0).getCity());
        assertNotNull(run.get(0).getCompanyName());
        assertNotNull(run.get(0).getCountry());
    }
}
