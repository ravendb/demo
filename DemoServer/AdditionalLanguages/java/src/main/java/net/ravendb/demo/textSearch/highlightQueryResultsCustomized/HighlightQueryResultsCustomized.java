package net.ravendb.demo.textSearch.highlightQueryResultsCustomized;
//region Usings
import net.ravendb.client.documents.indexes.AbstractIndexCreationTask;
import net.ravendb.client.documents.indexes.FieldIndexing;
import net.ravendb.client.documents.indexes.FieldStorage;
import net.ravendb.client.documents.indexes.FieldTermVector;
import net.ravendb.client.documents.queries.highlighting.HighlightingOptions;
import net.ravendb.client.documents.queries.highlighting.Highlightings;
import net.ravendb.client.documents.session.IDocumentSession;
import net.ravendb.client.primitives.Reference;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;

import java.util.List;

import static net.ravendb.client.documents.queries.Query.index;

public class HighlightQueryResultsCustomized {
    //region Demo
    //region Step_1
    public class EmployeesDetails extends AbstractIndexCreationTask {
    //endregion
        public EmployeesDetails() {
            //region Step_2
            map = "docs.Employees.Select(employee => new {\n" +
                "    title = employee.Title,\n" +
                "    notes = employee.Notes[0]\n" +
                // employee.Notes is a string array,
                // indexing only the first element for this example
                "})";
            //endregion

            //region Step_3
            store("title", FieldStorage.YES);
            index("title", FieldIndexing.SEARCH);
            termVector("title", FieldTermVector.WITH_POSITIONS_AND_OFFSETS);

            store("notes", FieldStorage.YES);
            index("notes", FieldIndexing.SEARCH);
            termVector("notes", FieldTermVector.WITH_POSITIONS_AND_OFFSETS);
            //endregion
        }
    }
    //endregion

    public List<Employee> run(RunParams runParams) {
        int fragmentLength = runParams.getFragmentLength();
        int fragmentCount = runParams.getFragmentCount();
        String tag1 = runParams.getTag1();
        String tag2 = runParams.getTag2();
        String tag3 = runParams.getTag3();
        String tag4 = runParams.getTag4();

        Reference<Highlightings> titleHighlightings = new Reference<>();
        Reference<Highlightings> notesHighlightings = new Reference<>();

        new EmployeesDetails().execute(DocumentStoreHolder.store);
        //region Demo
        List<Employee> employeesResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {

            //region Step_4
            HighlightingOptions tagsToUse1 = new HighlightingOptions();
            tagsToUse1.setPreTags(new String[] { tag1 });
            tagsToUse1.setPostTags(new String[] { tag2 });

            HighlightingOptions tagsToUse2 = new HighlightingOptions();
            tagsToUse2.setPreTags(new String[] { tag3 });
            tagsToUse2.setPostTags(new String[] { tag4 });
            //endregion

            //region Step_5
            employeesResults = session.query(Employee.class, index("EmployeesDetails"))
                .highlight("title", fragmentLength, fragmentCount, tagsToUse1, titleHighlightings)
                .highlight("notes", fragmentLength, fragmentCount, tagsToUse2, notesHighlightings)
                .search("title", "manager")
                .search("notes", "sales")
                .toList();
            //endregion

            //region Step_6
            if (employeesResults.size() > 0) {
                String employeeId = employeesResults.get(0).getId();
                String[] titleFragments = titleHighlightings.value.getFragments(employeeId);
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
        private String tag1;
        private String tag2;
        private String tag3;
        private String tag4;


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

        public String getTag1() {
            return tag1;
        }

        public void setTag1(String tag1) {
            this.tag1 = tag1;
        }

        public String getTag2() {
            return tag2;
        }

        public void setTag2(String tag2) {
            this.tag2 = tag2;
        }

        public String getTag3() {
            return tag3;
        }

        public void setTag3(String tag3) {
            this.tag3 = tag3;
        }

        public String getTag4() {
            return tag4;
        }

        public void setTag4(String tag4) {
            this.tag4 = tag4;
        }
    }
}

