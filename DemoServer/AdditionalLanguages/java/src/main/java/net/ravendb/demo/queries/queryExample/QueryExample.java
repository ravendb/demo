package net.ravendb.demo.queries.queryExample;

//region Usings
import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.documents.session.QueryStatistics;
import net.ravendb.client.primitives.Reference;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

import java.util.List;

public class QueryExample {

    public List<Employee> run() {
        //region Demo
        List<Employee> queryResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            Reference<QueryStatistics> statisticsRef = new Reference<>();

            //region Step_1
            IDocumentQuery<Employee> query = session.query(Employee.class)
            //endregion
                //region Step_2
                .whereEquals("FirstName", "Steven")
                .orElse()
                .whereEquals("Title", "Sales Representative")
                //endregion
                //region Step_3
                .include("ReportsTo")
                //endregion
                //region Step_4
                .statistics(statisticsRef)
                //endregion
                //region Step_5
                .orderByDescending("HiredAt")
                //endregion
                //region Step_6
                .take(5);
                //endregion

            //region Step_7
            queryResults = query.toList();
            //endregion
        }
        //endregion

        return queryResults;
    }
}
