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
import org.apache.commons.lang3.ObjectUtils;
import javax.xml.crypto.Data;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

public class HighlightQueryResultsCustomized {
    //region Demo
    //region Step_1
    public static class EmployeesDetails extends AbstractIndexCreationTask {
    //endregion
        public EmployeesDetails() {
            //region Step_2
            map = "docs.Employees.Select(employee => new {" +
                  "    Title = employee.Title," +
                  "    Notes = employee.Notes[0]" +
                       // employee.Notes is a string array,
                       // indexing only the first element for this example
                  "})";
            //endregion

            //region Step_3
            store("Title", FieldStorage.YES);
            index("Title", FieldIndexing.SEARCH);
            termVector("Title", FieldTermVector.WITH_POSITIONS_AND_OFFSETS);

            store("Notes", FieldStorage.YES);
            index("Notes", FieldIndexing.SEARCH);
            termVector("Notes", FieldTermVector.WITH_POSITIONS_AND_OFFSETS);
            //endregion
        }
    }
    //endregion

    public List<DataToShow> run(RunParams runParams) {
        int fragmentLength = ObjectUtils.firstNonNull(runParams.getFragmentLength(), 100);
        int fragmentCount = ObjectUtils.firstNonNull(runParams.getFragmentCount(), 1);

        String tag1 = ObjectUtils.firstNonNull(runParams.getTag1(), "+++");
        String tag2 = ObjectUtils.firstNonNull(runParams.getTag2(), "+++");
        String tag3 = ObjectUtils.firstNonNull(runParams.getTag3(), "<<<");
        String tag4 = ObjectUtils.firstNonNull(runParams.getTag4(), ">>>");

        Reference<Highlightings> titleHighlightings = new Reference<>();
        Reference<Highlightings> notesHighlightings = new Reference<>();
        //region Demo
        List<Employee> employeesResults;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
        
            //region Step_4
            HighlightingOptions tagsToUse1 = new HighlightingOptions();
            tagsToUse1.setPreTags(new String[]{ tag1 });
            tagsToUse1.setPostTags(new String[]{ tag2 });

            HighlightingOptions tagsToUse2 = new HighlightingOptions();
            tagsToUse2.setPreTags(new String[]{ tag3 });
            tagsToUse2.setPostTags(new String[]{ tag4 });
            //endregion

            //region Step_5
            employeesResults = session.query(Employee.class, EmployeesDetails.class)
                .highlight("Title", fragmentLength, fragmentCount, tagsToUse1, titleHighlightings)
                .highlight("Notes", fragmentLength, fragmentCount, tagsToUse2, notesHighlightings)
                .search("Title", "manager")
                .search("Notes", "sales")
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

        List<DataToShow> highlightResults = new ArrayList<>();

        for (Employee employee : employeesResults) {
            String[] titleFragments = titleHighlightings.value.getFragments(employee.getId());

            for (String item : titleFragments) {
                DataToShow itemResults = new DataToShow();
                itemResults.setDocumentId(employee.getId());
                itemResults.setIndexField(titleHighlightings.value.getFieldName());
                itemResults.setFragment(item);

                highlightResults.add(itemResults);
            }

            String[] notesFragments = notesHighlightings.value.getFragments(employee.getId());
            for (String item : notesFragments) {
                DataToShow itemResults = new DataToShow();
                itemResults.setDocumentId(employee.getId());
                itemResults.setIndexField(notesHighlightings.value.getFieldName());
                itemResults.setFragment(item);

                highlightResults.add(itemResults);
            }
        }

        return highlightResults.stream().sorted(Comparator.comparing(x -> x.indexField, Comparator.reverseOrder())).toList();
    }

    public static class RunParams {
        private Integer fragmentLength;
        private Integer fragmentCount;
        private String tag1;
        private String tag2;
        private String tag3;
        private String tag4;

        public Integer getFragmentLength() {
            return fragmentLength;
        }

        public void setFragmentLength(Integer fragmentLength) {
            this.fragmentLength = fragmentLength;
        }

        public Integer getFragmentCount() {
            return fragmentCount;
        }

        public void setFragmentCount(Integer fragmentCount) {
            this.fragmentCount = fragmentCount;
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

    public static class DataToShow {
        private String documentId;
        private String indexField;
        private String fragment;

        public String getDocumentId() {
            return documentId;
        }

        public void setDocumentId(String documentId) {
            this.documentId = documentId;
        }

        public String getIndexField() {
            return indexField;
        }

        public void setIndexField(String indexField) {
            this.indexField = indexField;
        }

        public String getFragment() {
            return fragment;
        }

        public void setFragment(String fragment) {
            this.fragment = fragment;
        }
    }
}
