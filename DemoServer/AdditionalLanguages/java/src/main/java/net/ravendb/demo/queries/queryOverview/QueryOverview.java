package net.ravendb.demo.queries.queryOverview;

import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

import java.util.List;

public class QueryOverview {
    public void run() {
        //region Demo
        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            IDocumentQuery<Employee> queryDefinition = session.query(Employee.class);
            //endregion

            //region Step_2
            // Define actions such as:

            // Filter documents by documents fields
            // Filter documents by text criteria
            // Include related documents
            // Get the query stats
            // Sort results
            // Customise the returned entity fields (Projections)
            // Control results paging
            //endregion

            //region Step_3
            List<Employee> queryResults = queryDefinition.toList();
            //endregion
        }
        //endregion
    }
}
