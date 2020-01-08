package net.ravendb.demo.queries.queryExample;

import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.QueryStatistics;
import net.ravendb.client.primitives.Reference;
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

import java.util.Date;
import java.util.List;

public class QueryExample {

    /**
     * NOTICE:
     * TODO:
     * please notice I skipped select part since we don't have LINQ in java,
     * Also employeeName can't be filled in as it is computed field
     *
     * I didn't put steps as they will be different than in c#.
     * Optionally we might present different query features here.
     */

    public void run() {
        //region Demo
        List<Employee> queryResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            Reference<QueryStatistics> statisticsRef = new Reference<>();
            IDocumentQuery<Employee> query = session.query(Employee.class)
                .whereEquals("FirstName", "Steven")
                .orElse()
                .whereEquals("Title", "Sales Representative")
                .include("ReportsTo")
                .statistics(statisticsRef)
                .orderByDescending("HiredAt")
                .take(5);

            queryResults = query.toList();
        }
        //endregion
    }

}
