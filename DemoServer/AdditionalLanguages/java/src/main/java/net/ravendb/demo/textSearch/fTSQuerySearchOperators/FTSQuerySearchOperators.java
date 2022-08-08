package net.ravendb.demo.textSearch.fTSQuerySearchOperators;
//region Usings
import net.ravendb.client.documents.queries.SearchOperator;
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import net.ravendb.demo.textSearch.fTSQuerySearchBasics.FTSQuerySearchBasics;

import java.util.List;

public class FTSQuerySearchOperators {
    public List<Employee> run(RunParams runParams) {
        String term1 = runParams.getTerm1();
        String term2 = runParams.getTerm2();
        String term3 = runParams.getTerm3();

        //region Demo
        List<Employee> employeesWithMatchingTerms;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            employeesWithMatchingTerms = session.query(Employee.class)
            //endregion
                //region Step_2
                .search("Notes", term1 + " " + term2, SearchOperator.AND)
                //endregion
                //region Step_3
                .search("Notes", term3, SearchOperator.OR)
                //endregion
                //region Step_4
                .toList();
                //endregion
        }
        //endregion
        return employeesWithMatchingTerms;
    }

    public static class RunParams {
        private String term1;
        private String term2;
        private String term3;


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

        public String getTerm3() {
            return term3;
        }

        public void setTerm3(String term3) {
            this.term3 = term3;
        }
    }
}
