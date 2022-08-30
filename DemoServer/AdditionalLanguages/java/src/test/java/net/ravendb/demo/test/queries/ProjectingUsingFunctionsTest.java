package net.ravendb.demo.test.queries;

import net.ravendb.demo.queries.projectingUsingFunctions.ProjectingUsingFunctions;
import org.junit.Test;
import java.util.List;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;

public class ProjectingUsingFunctionsTest {
    
    @Test
    public void test() {
        List<ProjectingUsingFunctions.EmployeeDetails> details = new ProjectingUsingFunctions().run();

        assertNotNull(details);
        assertEquals(9, details.size());

        assertNotNull(details.get(0).Title);
        assertNotNull(details.get(0).FullName);
    }
}
