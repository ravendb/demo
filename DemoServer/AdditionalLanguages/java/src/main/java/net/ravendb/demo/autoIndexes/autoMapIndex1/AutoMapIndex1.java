package net.ravendb.demo.autoIndexes.autoMapIndex1;

//region Usings
import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

public class AutoMapIndex1 {

    public Employee run(RunParams runParams) {
        String firstName = runParams.getFirstName();

        //region Demo
        Employee employeeResult;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            IDocumentQuery<Employee> findEmployeeQuery = session.query(Employee.class)
                .whereEquals("FirstName", firstName);
            //endregion

            //region Step_2
            employeeResult = findEmployeeQuery.firstOrDefault();
            //endregion
        }
        //endregion

        return employeeResult;
    }

    public static class RunParams {
        private String firstName;

        public String getFirstName() {
            return firstName;
        }

        public void setFirstName(String firstName) {
            this.firstName = firstName;
        }
    }
}
