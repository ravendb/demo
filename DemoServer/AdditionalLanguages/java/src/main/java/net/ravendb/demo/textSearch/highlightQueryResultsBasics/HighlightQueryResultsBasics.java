package net.ravendb.demo.textSearch.highlightQueryResultsBasics;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldIndexing;
import net.ravendb.client.documents.indexes.FieldStorage;
import net.ravendb.client.documents.indexes.FieldTermVector;
import net.ravendb.client.documents.queries.highlighting.Highlightings;
import net.ravendb.client.documents.session.IDocumentSession;
import static net.ravendb.client.documents.queries.Query.index;
import net.ravendb.client.primitives.Reference;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import java.util.List;

public class HighlightQueryResultsBasics {
    //region Demo
    //region Step_1
    public class EmployeesDetails extends AbstractIndexCreationTask {
    //endregion
        public EmployeesDetails() {
            //region Step_2
            map = "docs.Employees.Select(employee => new {\n" +
                "    Notes = employee.Notes[0]\n" +
                     // employee.Notes is a string array,
                     // indexing only the first element for this example
                "})";
            //endregion

            //region Step_3
            store("Notes", FieldStorage.YES);
            index("Notes", FieldIndexing.SEARCH);
            termVector("Notes", FieldTermVector.WITH_POSITIONS_AND_OFFSETS);
            //endregion
        }
    }
    //endregion

    public List<Employee> run(RunParams runParams) {
        int fragmentLength = runParams.getFragmentLength();
        int fragmentCount = runParams.getFragmentCount();
        Reference<Highlightings> notesHighlightings = new Reference<>();

        new EmployeesDetails().execute(DocumentStoreHolder.store);
        //region Demo
        List<Employee> employeesResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_4
            employeesResults = session.query(Employee.class, index("EmployeesDetails"))
                .highlight("Notes", fragmentLength, fragmentCount, notesHighlightings)
                .search("Notes", "sales")
                .toList();
            //endregion

            //region Step_5
            if (employeesResults.size() > 0) {
                String employeeId = employeesResults.get(0).getId();
                String[] notesFragments = notesHighlightings.value.getFragments(employeeId);
            }
            //endregion
        }
        //endregion
        return employeesResults;
    }

    public static class RunParams {
        private int fragmentLength;
        private int fragmentCount;


        public int getFragmentCount() {
            return fragmentCount;
        }

        public void setFragmentCount(int fragmentCount) {
            this.fragmentCount = fragmentCount;
        }

        public int getFragmentLength() {
            return fragmentLength;
        }

        public void setFragmentLength(int fragmentLength) {
            this.fragmentLength = fragmentLength;
        }
    }
}
