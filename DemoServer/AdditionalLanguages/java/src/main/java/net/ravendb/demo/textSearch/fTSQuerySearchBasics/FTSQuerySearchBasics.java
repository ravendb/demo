package net.ravendb.demo.textSearch.fTSQuerySearchBasics;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import java.util.List;

public class FTSQuerySearchBasics {

    public List<Employee> run(RunParams runParams) {
        String term1 = runParams.getTerm1();
        String term2 = runParams.getTerm2();

        //region Demo
        List<Employee> employeesWithMatchingTerms;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            employeesWithMatchingTerms = session.query(Employee.class)
            //endregion
                //region Step_2
                .search("Notes", term1 + " " + term2)
                //endregion
                //region Step_3
                .toList();
                //endregion
        }
        //endregion
        return employeesWithMatchingTerms;
    }

    public static class RunParams
    {
        private String term1;
        private String term2;


        public String getTerm1() {
            return term1;
        }

        public void setTerm1(String term1) {
            this.term1 = term1;
        }

        public String getTerm2() {
            return term2;
        }

        public void setTerm2(String term2) {
            this.term2 = term2;
        }
    }
}
