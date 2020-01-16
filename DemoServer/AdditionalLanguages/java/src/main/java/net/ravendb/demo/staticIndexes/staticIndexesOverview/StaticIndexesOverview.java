package net.ravendb.demo.staticIndexes.staticIndexesOverview;

//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.session.IDocumentQuery;
import net.ravendb.client.documents.session.IDocumentSession;
import java.util.List;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

public class StaticIndexesOverview {

    //region Demo
    //region Step_1
    public static class Employees_ByLastName extends AbstractIndexCreationTask {
    //endregion

        //region Step_2
        public Employees_ByLastName() {
            // Define:
            //    Map(s) functions
            //    Reduce function
            //    Additional indexing options per field
        }
        //endregion
    }
    //endregion

    public void run() {
        //region Demo
        List<Employee> queryResults;

        //region Step_3
        new Employees_ByLastName().execute(DocumentStoreHolder.store);
        //endregion

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_4
            IDocumentQuery<Employee> queryOnIndex = session.query(Employee.class, Employees_ByLastName.class)
                .whereEquals("LastName", "SomeName");

            queryResults = queryOnIndex.toList();
            //endregion
        }
        //endregion
    }
}
