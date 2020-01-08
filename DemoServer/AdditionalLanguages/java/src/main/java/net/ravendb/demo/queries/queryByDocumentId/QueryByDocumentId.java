package net.ravendb.demo.queries.queryByDocumentId;

import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

public class QueryByDocumentId {

    public Employee run(RunParams runParams) {
        String employeeDocumentId = runParams.getEmployeeDocumentId();
        //region Demo
        Employee employee;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            IDocumentQuery<Employee> queryByDocumentId = session.query(Employee.class)
            //endregion
            //region Step_2
                .whereEquals("id", employeeDocumentId);
            //endregion

            //region Step_3
            employee = queryByDocumentId.firstOrDefault();
            //endregion
        }
        //endregion

        return employee;
    }

    public static class RunParams {
        private String employeeDocumentId;

        public String getEmployeeDocumentId() {
            return employeeDocumentId;
        }

        public void setEmployeeDocumentId(String employeeDocumentId) {
            this.employeeDocumentId = employeeDocumentId;
        }
    }
}
