package net.ravendb.demo.queries.filteringResultsMultipleConditions;

//region Usings
import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.Arrays;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

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
                .whereIn("FirstName", Arrays.asList("Anne", "John"))
                .orElse()
                .openSubclause()
                .whereEquals("Address.Country", country)
                .whereGreaterThan("Territories.Count", 2)
                .whereStartsWith("Title", "Sales")
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
