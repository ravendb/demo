package net.ravendb.demo.test.queries;

import net.ravendb.demo.queries.projectingIndividualFields.ProjectingIndividualFields;
import org.junit.Test;
import java.util.List;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;

public class ProjectingIndividualFieldsTest {
    
    @Test
    public void test() {
        List<ProjectingIndividualFields.CompanyDetails> details = new ProjectingIndividualFields().run();

        assertNotNull(details);
        assertEquals(91, details.size());

        assertNotNull(details.get(0).getCity());
        assertNotNull(details.get(0).getCompanyName());
        assertNotNull(details.get(0).getCountry());
    }
}
