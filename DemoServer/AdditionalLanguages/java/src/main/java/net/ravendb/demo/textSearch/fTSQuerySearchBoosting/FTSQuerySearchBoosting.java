package net.ravendb.demo.textSearch.fTSQuerySearchBasics;

//region Usings
import net.ravendb.client.documents.session.IDocumentSession;
//endregion
import net.ravendb.demo.common.DocumentStoreHolder;
import net.ravendb.demo.common.models.Employee;
import java.util.List;

public class FTSQuerySearchBoosting {

    public List<Employee> run(RunParams runParams) {
        double boost1 = runParams.getBoost1();
        double boost2 = runParams.getBoost2();
        double boost3 = runParams.getBoost3();

        //region Demo
        List<Employee> employeesWithMatchingTerms;

        try (IDocumentSession session = DocumentStoreHolder.store.openSession()) {
            //region Step_1
            employeesWithMatchingTerms = session.query(Employee.class)
            //endregion
                //region Step_2
                .search("Notes", "ph.d.").boost(boost1)
                .search("Notes", "university").boost(boost2)
                .search("Notes", "college").boost(boost3)
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
        private double boost1;
        private double boost2;
        private double boost3;

        public double getBoost1() {
            return boost1;
        }

        public void setBoost1(int boost1) {
            this.boost1 = boost1;
        }

        public double getBoost2() {
            return boost2;
        }

        public void setBoost2(int boost2) {
            this.boost2 = boost2;
        }

        public double getBoost3() {
            return boost3;
        }

        public void setBoost3(int boost3) {
            this.boost3 = boost3;
        }
    }
}
