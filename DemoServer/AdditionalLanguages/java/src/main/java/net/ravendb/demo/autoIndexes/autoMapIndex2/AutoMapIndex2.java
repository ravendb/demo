package net.ravendb.demo.autoIndexes.autoMapIndex2;

import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Company;
import net.ravendb.demo.common.models.Employee;

public class AutoMapIndex2 {

    public void run(RunParams runParams) {
        String country = runParams.getCountry();

        //region Demo
        Employee employeeResult;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            IDocumentQuery<Employee> findEmployeeQuery = session.query(Employee.class)
                .whereEquals("Address.Country", country)
                .whereStartsWith("Title", "Sales");
            //endregion

            //region Step_2
            employeeResult = findEmployeeQuery.firstOrDefault();
            //endregion
        }
        //endregion
    }

    public static class RunParams {
        private String country;

        public String getCountry() {
            return country;
        }

        public void setCountry(String country) {
            this.country = country;
        }
    }
}
