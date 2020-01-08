package net.ravendb.demo.queries.filteringResultsMultipleConditions;

import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

import java.util.Arrays;
import java.util.List;

public class FilteringResultsMultipleConditions {

    public void run(RunParams runParams) {
        String country = runParams.getCountry();

        //region Demo
        List<Employee> filteredEmployees;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            IDocumentQuery<Employee> filteredQuery = session.query(Employee.class)
            //endregion
            //region Step_2
                .whereIn("firstName", Arrays.asList("Anne", "John"))
                .orElse()
                .openSubclause()
                .whereEquals("address.country", country)
                .whereGreaterThan("territories.Count", 2)
                .whereStartsWith("title", "Sales")
                .closeSubclause();
            //endregion

            //region Step_3
            filteredEmployees = filteredQuery.toList();
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
