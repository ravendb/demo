package net.ravendb.demo.queries.filteringResultsBasics;

//region Usings
import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

public class FilteringResultsBasics {
    public List<Employee> run() {
        //region Demo
        List<Employee> filteredEmployees;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            IDocumentQuery<Employee> filteredQuery = session.query(Employee.class)
            //endregion
                //region Step_2
                .whereEquals("FirstName", "Anne");
                //endregion

            //region Step_3
            filteredEmployees = filteredQuery.toList();
            //endregion
        }
        //endregion

        return filteredEmployees;
    }
}
